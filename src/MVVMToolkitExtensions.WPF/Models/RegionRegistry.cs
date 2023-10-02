using CommunityToolkit.Mvvm.Messaging;
using MVVMToolkitExtensions.WPF.Controls;
using MVVMToolkitExtensions.WPF.Interfaces;

namespace MVVMToolkitExtensions.WPF.Models;

internal class RegionRegistry : IRegionRegistry, IRecipient<RegisterRegionMessage>
{
    private readonly Dictionary<string, RegionControl> _registry;

    public RegionRegistry()
    {
        _registry = new();
        WeakReferenceMessenger.Default.Register(this);
    }

    public RegionControl this[string regionName]
    {
        get => GetValueOrNull(regionName) ?? throw new KeyNotFoundException($"Region {regionName} not found.");
        set => _registry[regionName] = value;
    }

    public bool Contains(string regionName)
        => _registry.ContainsKey(regionName);

    public void Receive(RegisterRegionMessage message)
        => Add(message.Value.Name, message.Value.Region);

    private RegionControl? GetValueOrNull(string regionName)
        => _registry.TryGetValue(regionName, out var region) ? region : null;

    private void Add(string regionName, RegionControl region)
        => _registry.Add(regionName, region);
}
