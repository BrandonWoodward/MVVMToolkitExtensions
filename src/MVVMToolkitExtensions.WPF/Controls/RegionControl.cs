using System.Windows;
using System.Windows.Controls;
using MVVMToolkitExtensions.Core.Interfaces;
using MVVMToolkitExtensions.WPF.Interfaces;

namespace MVVMToolkitExtensions.WPF.Controls;

public class RegionControl : ContentControl
{
    public static readonly DependencyProperty RegionNameProperty = DependencyProperty.Register(
        nameof(RegionName),
        typeof(string),
        typeof(RegionControl),
        new(null, OnRegionNameChanged));

    public string RegionName 
    {
        get => (string)GetValue(RegionNameProperty);
        set => SetValue(RegionNameProperty, value);
    }

    private static void OnRegionNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) 
    {
        if (d is not RegionControl navigationContainer || e.NewValue is not string regionName) return;
        
        // TODO - How can I access the INavigationRegistry here without this global state?
        if (Application.Current.Resources["NavigationRegistry"] 
            is not IRegionRegistry<RegionControl> navigationRegistry) 
            throw new NullReferenceException();
        
        navigationRegistry[regionName] = navigationContainer;
    }
}
