# ColorManager

A GTK-based color palette manager application for managing and organizing color palettes. Built with .NET 8.0 and GTK# 3.

## Overview

ColorManager is a desktop application that allows users to:
- Create and manage color palettes
- Store color palettes in XML format
- Select, edit, and delete color palettes
- View and interact with multiple colors per palette (up to 10 colors per palette)
- Display color codes in hex format

## Features

- **Palette Management**: Create new color palettes with custom names
- **Color Selection**: Support for up to 10 colors per palette
- **Persistent Storage**: Color palettes are stored in `colors.xml` for persistence
- **Edit & Delete**: Easily modify or remove existing palettes
- **Color Display**: Visual color buttons and hex color code input/display

## System Requirements

- .NET 8.0 Runtime or SDK
- GTK+ 3.0 or later (for GTK# bindings)
- Linux, Windows, or macOS with .NET 8.0 support

## Project Structure

```
ColorManager/
├── Main.cs                  # Application entry point
├── MainWindow.cs           # Main window UI (legacy)
├── MyForm.cs               # Primary application UI
├── MyColor.cs              # Color model class
├── PaletteManager.cs       # Palette management logic
├── AssemblyInfo.cs         # Assembly metadata
├── colors.xml              # Palette storage file
├── ColorManager.csproj     # Project configuration
└── gtk-gui/                # GTK UI designer files
    ├── gui.stetic          # Stetic UI definition
    ├── generated.cs        # Auto-generated UI code
    └── MainWindow.cs       # Auto-generated window code
```

## Building the Project

### Prerequisites

Install the .NET SDK 8.0 and required dependencies:

```bash
# On Debian/Ubuntu
sudo apt-get install dotnet-sdk-8.0 libgtk-3-dev

# On Fedora
sudo dnf install dotnet-sdk-8.0 gtk3-devel

# On macOS with Homebrew
brew install dotnet gtk+3
```

### Build

```bash
# Navigate to the project directory
cd /workspaces/ColorManager

# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Build in Release mode
dotnet build --configuration Release
```

## Running the Application

```bash
# Run the application
dotnet run

# Or run the compiled executable directly
./ColorManager/bin/Debug/net8.0/ColorManager
```

## Development

### Technologies Used

- **Language**: C#
- **Framework**: .NET 8.0
- **GUI Framework**: GTK# 3 (GtkSharp 3.24.24.117-develop)
- **Data Format**: XML (colors.xml)

### Key Classes

- **MyForm**: Main application window and UI logic
- **PaletteManager**: Manages palette CRUD operations and XML storage
- **MyColor**: Color data model

### Code Style

The project follows C# naming conventions with PascalCase for methods and properties.

## Recent Updates

### Version 2.0.0 (Current)

- Updated to .NET 8.0
- Updated GTK# to latest develop version (3.24.24.117-develop)
- Modernized project file format (SDK-style .csproj)
- Fixed deprecated GTK# API calls (e.g., `RemoveText` → `Remove`, `ComboBoxEntry` → `ComboBoxText`)
- Removed `Mono.Unix.Catalog` localization calls in favor of direct strings
- Fixed icon loading for better compatibility

## Troubleshooting

### GTK Initialization Errors

If you encounter GTK initialization errors, ensure GTK+ 3.0 is properly installed:

```bash
# Check GTK installation
pkg-config --modversion gtk+-3.0
```

### Missing Colors.xml

The application will use the embedded `colors.xml` file. Ensure it exists in the output directory:

```bash
# File should be copied during build to:
./ColorManager/bin/Debug/net8.0/colors.xml
```

## Contributing

When making changes to the project:
1. Update the version in `AssemblyInfo.cs` if necessary
2. Follow C# naming conventions
3. Test on both Debug and Release configurations
4. Update this README if adding new features

## License

This project is licensed under the GNU General Public License v3.0. See the LICENSE file for details.

## Author

Original project by Sharique Ahmed Farooqui

## Support

For issues and questions, please refer to the project repository.
