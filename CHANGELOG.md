## 0.1.0 - 2026-05-19

### Loader
- Added the initial BepInEx bridge that loads `Libraries\ResoniteModLoader.dll`.
- Loads ResoniteModLoader through BepInEx's runtime load context.
- Leaves ResoniteModLoader itself separate, so the package does not bundle or replace RML.

### Build And Packaging
- Added DesktopBuddy-style local build/deploy support with Gale profile detection and `-Restart`.
- Added separate manual GitHub release and Thunderstore package layouts.
- Added GitHub release notes that put README install instructions above the current version changelog.
- Added Thunderstore publishing support through `tcli`.
