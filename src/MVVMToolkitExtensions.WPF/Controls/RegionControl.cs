using CommunityToolkit.Mvvm.Messaging;
using MVVMToolkitExtensions.WPF.Models;
using System.Windows;
using System.Windows.Controls;

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
        if(d is not RegionControl navigationContainer || e.NewValue is not string regionName) return;
        WeakReferenceMessenger.Default.Send(new RegisterRegionMessage((regionName, navigationContainer)));
    }
}
