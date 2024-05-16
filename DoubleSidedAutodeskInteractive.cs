Shader "Custom/DoubleSidedAutodeskInteractive"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Main Texture", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _BumpMap ("Normal Map", 2D) = "bump" {}
        _OcclusionStrength ("Occlusion Strength", Range(0,1)) = 1.0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float3 normal : TEXCOORD1;
                float4 pos : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _BumpMap;
            float4 _MainTex_ST;
            float4 _Color;
            float _Glossiness;
            float _Metallic;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;

                // Normal mapping
                fixed3 normal = normalize(i.normal);
                fixed3 bump = UnpackNormal(tex2D(_BumpMap, i.uv));
                normal = normalize(normal + bump);

                // Metallic and Smoothness
                fixed3 albedo = col.rgb;
                fixed metallic = _Metallic;
                fixed smoothness = _Glossiness;

                // Lighting calculation
                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.rgb;
                fixed3 lightDir = normalize(UnityWorldSpaceLightDir(i.pos));
                fixed3 viewDir = normalize(UnityWorldSpaceViewDir(i.pos));
                fixed3 halfDir = normalize(lightDir + viewDir);
                fixed NdotL = max(0, dot(normal, lightDir));
                fixed NdotV = max(0, dot(normal, viewDir));
                fixed NdotH = max(0, dot(normal, halfDir));
                fixed3 diffuse = NdotL * _LightColor0.rgb;

                // Specular calculation
                fixed spec = pow(NdotH, 4.0 / (smoothness + 0.001));
                fixed3 specular = spec * _LightColor0.rgb;

                fixed3 finalColor = (ambient + diffuse + specular) * albedo;

                return fixed4(finalColor, col.a);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
