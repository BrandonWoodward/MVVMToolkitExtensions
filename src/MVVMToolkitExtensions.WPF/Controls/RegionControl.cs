using CommunityToolkit.Mvvm.Messaging;
using MVVMToolkitExtensions.WPF.Models;
using System.Windows;
using System.Windows.Controls;

namespace MVVMToolkitExtensions.WPF.Controls;

/// <summary>
/// Represents a control that provides a region for view-based navigation.
/// This control allows you to define a region in XAML and then use a navigation 
/// service to place views into it.
/// </summary>
/// <remarks>
/// By defining a <see cref="RegionName"/>, you're effectively creating a placeholder
/// or "region" where content can be injected dynamically using the navigation service.
/// </remarks>
/// <example>
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
