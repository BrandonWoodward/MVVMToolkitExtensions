using MVVMToolkitExtensions.Core.Interfaces;

namespace MVVMToolkitExtensions.MAUI.Controls;

/// <summary>
/// Supports view-based navigation by providing a region to fill with content.
/// </summary>
public class RegionControl : ContentView
{
    public static readonly BindableProperty RegionNameProperty =
        BindableProperty.Create(
            nameof(RegionName),
            typeof(string),
            typeof(RegionControl),
            defaultValue: null,
            propertyChanged: OnRegionNameChanged);

    public string RegionName
    {
        get => (string)GetValue(RegionNameProperty);
        set => SetValue(RegionNameProperty, value);
    }

    private static void OnRegionNameChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not RegionControl navigationContainer || newValue is not string regionName) return;
        
        // This is strange. How else to get the service provider in a static callback?
        // All controls in MAUI have an IMauiContext from which you can access the Dependency Injection container to get services.
        var regionRegistry = (navigationContainer as IMauiContext)!.Services.GetRequiredService<IRegionRegistry<RegionControl>>();
        regionRegistry[regionName] = navigationContainer;
    }
}
