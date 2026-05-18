using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using BepInEx;
using BepInEx.NET.Common;
using BepInExResoniteShim;
using BepisResoniteWrapper;

namespace ResoniteModLoaderLoader;

[ResonitePlugin(PluginGuid, PluginName, PluginVersion, PluginAuthor, PluginUrl)]
[BepInPlugin(PluginGuid, PluginName, PluginVersion)]
[BepInDependency(BepInExResoniteShim.PluginMetadata.GUID, BepInDependency.DependencyFlags.HardDependency)]
public sealed class ResoniteModLoaderLoaderPlugin : BasePlugin
{
    private const string PluginGuid = "com.devl0rd.ResoniteModLoaderLoader";
    private const string PluginName = "Resonite Mod Loader Loader";
    private const string PluginAuthor = "DevL0rd";
    private const string PluginVersion = VersionInfo.Version;
    private const string PluginUrl = "https://github.com/DevL0rd/ResoniteModLoaderLoader";
    private const string RmlAssemblyName = "ResoniteModLoader";
    private const string RmlRelativePath = @"Libraries\ResoniteModLoader.dll";

    public override void Load()
    {
        if (IsResoniteModLoaderAlreadyLoaded())
        {
            Log.LogInfo("ResoniteModLoader is already loaded; nothing to do.");
            return;
        }

        var rmlPath = ResolveResoniteModLoaderPath();
        if (rmlPath == null)
        {
            Log.LogError($"Could not find {RmlRelativePath}. Install ResoniteModLoader.dll into Resonite's Libraries folder.");
            return;
        }

        var loadContext = GetBepInExLoadContext();
        if (loadContext == null)
        {
            Log.LogError("BepInEx runtime LoadContext is unavailable; ResoniteModLoader will not be loaded.");
            return;
        }

        try
        {
            InstallAssemblyResolver();
            loadContext.LoadFromAssemblyPath(rmlPath);
            Log.LogInfo($"Loaded ResoniteModLoader from {rmlPath}");
        }
        catch (Exception ex)
        {
            Log.LogError($"Failed to load ResoniteModLoader from {rmlPath}: {ex}");
        }
    }

    private static bool IsResoniteModLoaderAlreadyLoaded()
    {
        return AppDomain.CurrentDomain
            .GetAssemblies()
            .Any(assembly => string.Equals(assembly.GetName().Name, RmlAssemblyName, StringComparison.Ordinal));
    }

    private static string ResolveResoniteModLoaderPath()
    {
        foreach (var root in GetCandidateRoots())
        {
            if (string.IsNullOrWhiteSpace(root))
                continue;

            var candidate = Path.GetFullPath(Path.Combine(root, RmlRelativePath));
            if (File.Exists(candidate))
                return candidate;
        }

        return null;
    }

    private static string[] GetCandidateRoots()
    {
        return new[]
        {
            AppContext.BaseDirectory,
            Directory.GetCurrentDirectory(),
            Path.GetDirectoryName(typeof(ResoniteModLoaderLoaderPlugin).Assembly.Location)
        };
    }

    private static void InstallAssemblyResolver()
    {
        foreach (var context in AssemblyLoadContext.All)
        {
            context.Resolving += ResolveFromLoadedAssemblies;
        }
    }

    private static AssemblyLoadContext GetBepInExLoadContext()
    {
        var utilityType = Type.GetType("BepInEx.Utility, BepInEx.Core");
        var field = utilityType?.GetField("LoadContext", BindingFlags.Public | BindingFlags.Static);
        return field?.GetValue(null) as AssemblyLoadContext;
    }

    private static Assembly ResolveFromLoadedAssemblies(AssemblyLoadContext context, AssemblyName assemblyName)
    {
        return AppDomain.CurrentDomain
            .GetAssemblies()
            .FirstOrDefault(assembly => string.Equals(assembly.GetName().Name, assemblyName.Name, StringComparison.Ordinal));
    }
}
