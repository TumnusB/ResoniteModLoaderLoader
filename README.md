# Resonite Mod Loader Loader

<p align="center">
  <img src="icon.png" alt="Resonite Mod Loader Loader icon" width="512">
</p>

Resonite Mod Loader Loader is a tiny BepInEx plugin that loads Resonite Mod Loader for you.

## Install

### Easy Install

1. Follow the Resonite modding setup instructions for Gale and BepisLoader:
https://modding.resonite.net/getting-started/installation/

2. Install ResoniteModLoader's normal files into the Resonite install folder:

```text
C:\Program Files (x86)\Steam\steamapps\common\Resonite\Libraries\ResoniteModLoader.dll
C:\Program Files (x86)\Steam\steamapps\common\Resonite\rml_libs\0Harmony.dll
```

3. Search for Resonite Mod Loader Loader in Gale and enable the mod.

4. Launch Resonite with Gale.

Thunderstore packages update more slowly because every release can require review.

### Manual Install

Manual GitHub release zips are the bleeding-edge path. They include the BepInEx loader plugin in one self-contained zip, but they do not include ResoniteModLoader itself.

1. Download `ResoniteModLoaderLoader-x.y.z.zip` from the latest [GitHub release](https://github.com/DevL0rd/ResoniteModLoaderLoader/releases), then extract it into the correct root folder. The zip contains the `BepInEx` folder used by the manual install layout.

2. Choose install method:
For Gale, extract into the profile root:

```text
%APPDATA%\com.kesomannen.gale\resonite\profiles\Default
```

For another Gale profile, replace `Default` with that profile folder name.

For a manual BepisLoader install, extract into the Resonite install folder:

```text
C:\Program Files (x86)\Steam\steamapps\common\Resonite
```

For manual installs, launch Resonite with BepisLoader enabled, such as with `--hookfxr-enable`.

Install or enable these loader packages too:

- BepisLoader
- BepisResoniteWrapper

Install ResoniteModLoader's normal files separately:

- `Libraries\ResoniteModLoader.dll`
- `rml_libs\0Harmony.dll`
- optional mods in `rml_mods`


## Features

- Loads `Libraries\ResoniteModLoader.dll` from the Resonite install folder after BepInEx starts.
- Lets ResoniteModLoader start without keeping `-LoadAssembly Libraries/ResoniteModLoader.dll` in Steam launch options.
- Avoids bundling or replacing ResoniteModLoader, so your RML install stays separate.
- Uses BepInEx's runtime load context for the RML assembly.
- Stops with a clear BepInEx log error if the expected RML file or BepInEx load context is missing.


## How It Works

On plugin load, it checks whether `ResoniteModLoader` is already present in the current AppDomain. If not, it looks for:

```text
<Resonite install>\Libraries\ResoniteModLoader.dll
```

Then it loads that assembly. ResoniteModLoader's module initializer starts RML from there.

The assembly is loaded through BepInEx's runtime load context. If that context is unavailable, the plugin logs an error and does not load RML. That mirrors the important part of Nytra's MonkeyLoaderLoader approach and avoids splitting shared assemblies across separate .NET load contexts.


## Building

Install:

- .NET 10 SDK
- Windows SDK 10.0.26100.0 or newer

Build locally:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File scripts\build.ps1 -Restart
```

This builds the BepInEx plugin, deploys it into the local Gale profile named `Default` when present, and restarts Resonite through the root HookFxr loader with that profile's BepInEx target. Add `-Desktop` for desktop mode:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File scripts\build.ps1 -Restart -Desktop
```

Use a different Gale profile name with `-ProfileName`, or an exact profile path with `-ProfilePath`:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File scripts\build.ps1 -Restart -ProfileName MyProfile
powershell -NoProfile -ExecutionPolicy Bypass -File scripts\build.ps1 -Restart -ProfilePath "$env:APPDATA\com.kesomannen.gale\resonite\profiles\MyProfile"
```

CI-style compile without deploy:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File scripts\build.ps1 -NoDeploy
```


## Packaging And Release

Thunderstore package metadata lives in `thunderstore.toml`, while `scripts\package.ps1` builds a clean GitHub release zip for manual installation into a Gale profile or manual BepisLoader root. `VERSION` is the source of truth for the plugin package. After changing it, run:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File scripts\sync-version.ps1
```

Create the manual GitHub release zip locally with:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File scripts\package.ps1
```

To build the Thunderstore package for manager/testing use, add `-ThunderstoreFormat`:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File scripts\package.ps1 -ThunderstoreFormat
```

GitHub Actions refreshes the manual release zip on pushes to `main`, even when `VERSION` does not change. Thunderstore publishing is separate and only uploads exact package versions that do not already exist.


## Credits

Special thanks to the projects Resonite Mod Loader Loader builds on.

| Project | What Resonite Mod Loader Loader uses it for |
| --- | --- |
| [BepisLoader](https://thunderstore.io/c/resonite/p/ResoniteModding/BepisLoader/) | Game-side BepInEx loader |
| [BepisResoniteWrapper](https://github.com/ResoniteModding/BepisResoniteWrapper) | Resonite BepInEx plugin support |
| [ResoniteModLoader](https://github.com/resonite-modding-group/ResoniteModLoader) | The mod loader assembly this plugin loads |
| [MonkeyLoaderLoader-BepInEx](https://github.com/Nytra/MonkeyLoaderLoader-BepInEx) | Reference point for loading another mod loader through BepInEx |


## License

AGPL-3.0 - see [LICENSE](LICENSE).
