using System.Windows;
using System.Windows.Controls;
using MVVMToolkitExtensions.WPF.Interfaces;

namespace MVVMToolkitExtensions.WPF.Controls;

public class NavigationContainer : ContentControl
{
    public static readonly DependencyProperty RegionNameProperty = DependencyProperty.Register(
        nameof(RegionName),
        typeof(string),
        typeof(NavigationContainer),
        new(null, OnRegionNameChanged));

    public string RegionName 
    {
        get => (string)GetValue(RegionNameProperty);
        set => SetValue(RegionNameProperty, value);
    }

    private static void OnRegionNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) 
    {
        if (d is not NavigationContainer navigationContainer || e.NewValue is not string regionName) return;
        
        // TODO - How can I access the INavigationRegistry here without this global state?
        if (Application.Current.Resources["NavigationRegistry"] 
            is not INavigationRegistry navigationRegistry) 
            throw new NullReferenceException();
        
        navigationRegistry[regionName] = navigationContainer;
    }
}
