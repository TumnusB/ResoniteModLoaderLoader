param([string]$Root = (Resolve-Path (Join-Path $PSScriptRoot "..")).Path)

$ErrorActionPreference = "Stop"

$versionPath = Join-Path $Root "VERSION"
$tomlPath = Join-Path $Root "thunderstore.toml"
$version = (Get-Content -Raw -LiteralPath $versionPath).Trim()
$toml = Get-Content -Raw -LiteralPath $tomlPath

if ($version -notmatch '^\d+\.\d+\.\d+$') {
    throw "VERSION must be semantic Major.Minor.Patch without suffix. Current value: '$version'"
}

$updated = [regex]::Replace($toml, '(?m)^versionNumber\s*=\s*"[^"]*"', "versionNumber = `"$version`"")
if ($updated -eq $toml) {
    Write-Host "ResoniteModLoaderLoader version already synced to $version"
    return
}

Set-Content -NoNewline -LiteralPath $tomlPath -Value $updated
Write-Host "ResoniteModLoaderLoader version synced to $version"
