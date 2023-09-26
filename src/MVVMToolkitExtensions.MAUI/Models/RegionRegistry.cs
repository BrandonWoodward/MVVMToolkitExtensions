using MVVMToolkitExtensions.MAUI.Controls;
using MVVMToolkitExtensions.MAUI.Interfaces;

namespace MVVMToolkitExtensions.MAUI.Models;

internal sealed class RegionRegistry : IRegionRegistry
{
    private readonly Dictionary<string, RegionControl> _registry = new();

    public RegionControl this[string regionName]
    {
        get => _registry[regionName];
        set => _registry[regionName] = value;
    }

    public bool Contains(string regionName)
        => _registry.ContainsKey(regionName);
}
