# Resonite Mod Loader Loader

Resonite Mod Loader Loader is a tiny BepInEx plugin that loads Resonite Mod Loader for you.


## Install

1. Follow the Resonite modding setup instructions for Gale and BepisLoader:
https://modding.resonite.net/getting-started/installation/

2. Install ResoniteModLoader's normal files into the Resonite install folder:

- `Libraries\ResoniteModLoader.dll`
- `rml_libs\0Harmony.dll`
- optional mods in `rml_mods`

3. Search for Resonite Mod Loader Loader and enable the mod.

4. Launch Resonite with Gale.

GitHub release zips are the bleeding-edge manual install path and may update faster than Thunderstore while packages wait for review.


## Features

- Loads `Libraries\ResoniteModLoader.dll` from the Resonite install folder after BepInEx starts.
- Lets ResoniteModLoader start without keeping `-LoadAssembly Libraries/ResoniteModLoader.dll` in Steam launch options.
- Avoids bundling or replacing ResoniteModLoader, so your RML install stays separate.
- Uses BepInEx's runtime load context for the RML assembly.
- Stops with a clear BepInEx log error if the expected RML file or BepInEx load context is missing.


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
