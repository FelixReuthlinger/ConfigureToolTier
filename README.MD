# Configure Tool Tier

Valheim mod to let you configure the tool tier required to harvest wood or stone.

For more details see [README](ConfigureToolTier).

## Building requires

BepInEx installation at your vanilla Valheim folder, the build will publish the dll there automatically.

Add `Environment.props` file at root folder with content (modify path if used different install path):
```
<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="Current" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
    <PropertyGroup>
        <!-- Needs to be your path to the base Valheim folder -->
        <VALHEIM_INSTALL>C:\Program Files (x86)\Steam\steamapps\common\Valheim</VALHEIM_INSTALL>
        <!-- This is the folder where your build gets copied to when using the post-build automations -->
        <MOD_DEPLOYPATH>C:\Users\felix\AppData\Roaming\Thunderstore Mod Manager\DataFolder\Valheim\profiles\configuretooltier\BepInEx\plugins</MOD_DEPLOYPATH>
    </PropertyGroup>
</Project>
```
