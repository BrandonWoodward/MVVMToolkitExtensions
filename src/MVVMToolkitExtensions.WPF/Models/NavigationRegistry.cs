using MVVMToolkitExtensions.WPF.Controls;
using MVVMToolkitExtensions.WPF.Interfaces;

namespace MVVMToolkitExtensions.WPF.Models;

public sealed class NavigationRegistry : INavigationRegistry
{
    private readonly Dictionary<string, NavigationContainer> _registry = new();

    public NavigationContainer this[string regionName]
    {
        get => _registry[regionName];
        set => _registry[regionName] = value;
    }

    public bool Contains(string regionName)
        => _registry.ContainsKey(regionName);
}
