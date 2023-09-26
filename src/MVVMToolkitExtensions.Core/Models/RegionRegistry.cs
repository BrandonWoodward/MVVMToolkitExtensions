using MVVMToolkitExtensions.Core.Interfaces;

namespace MVVMToolkitExtensions.Core.Models;

internal class RegionRegistry<TRegionControl> : IRegionRegistry<TRegionControl>
    where TRegionControl : class
{
    private readonly Dictionary<string, TRegionControl> _registry = new();

    public TRegionControl this[string regionName]
    {
        get => _registry.TryGetValue(regionName, out var region) 
            ? region : throw new KeyNotFoundException("Region not found. Use the RegionControl in your XAML to register a region.");
        set => _registry[regionName] = value;
    }

    public bool Contains(string regionName)
        => _registry.ContainsKey(regionName);
}