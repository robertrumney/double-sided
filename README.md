# DoubleSidedShader

Custom Unity shader rendering both sides of geometry.

## Description

This repository contains a custom shader for Unity that renders both sides of the geometry using the Autodesk Interactive shader as a base. The shader includes normal mapping and uses the standard lighting model with support for forward rendering and shadows.

## Features

- **Double-sided rendering**: Both sides of the polygons are rendered.
- **Standard lighting model**: Utilizes Unity's Standard Surface Shader for realistic lighting.
- **Normal mapping**: Supports normal maps for detailed surface textures.
- **Configurable properties**: Color, Main Texture, Smoothness, Metallic, Normal Map, and Occlusion Strength.

## Properties

- **_Color**: The base color of the material.
- **_MainTex**: The main texture applied to the material.
- **_Glossiness**: Controls the smoothness of the material.
- **_Metallic**: Controls the metallic look of the material.
- **_BumpMap**: The normal map for the material.
- **_OcclusionStrength**: Controls the strength of the occlusion effect.

## Usage

1. Create a new shader in Unity and name it `DoubleSidedAutodeskInteractive`.
2. Copy the shader code from `DoubleSidedAutodeskInteractive.shader` in this repository and paste it into the new shader.
3. Create a new material in Unity and assign the `DoubleSidedAutodeskInteractive` shader to it.
4. Customize the material properties as needed.
5. Assign the material to your 3D model.

## Contributing

Contributions are welcome! Please open an issue or submit a pull request for any changes or improvements.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
