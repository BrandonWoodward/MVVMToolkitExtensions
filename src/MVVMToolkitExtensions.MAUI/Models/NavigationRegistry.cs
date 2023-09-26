using MVVMToolkitExtensions.MAUI.Interfaces;

namespace MVVMToolkitExtensions.MAUI.Models;

internal sealed class NavigationRegistry : INavigationRegistry
{
    private readonly Dictionary<string, Type> _registry;
    
    public Type this[string route] 
        => _registry.TryGetValue(route, out var type) 
            ? type : throw new ArgumentException($"Route '{route}' not found. Make sure you have registered " +
                                                 $"the route using builder.Services.AddRoute<TView>().");
    

    public NavigationRegistry(IEnumerable<INavigationRegistration> navigationRegistrations)
    {
        _registry = navigationRegistrations.ToDictionary(
            navigationRegistration => navigationRegistration.Route,
            navigationRegistration => navigationRegistration.PageType
        );
    }
}