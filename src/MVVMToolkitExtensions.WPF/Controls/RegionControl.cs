using CommunityToolkit.Mvvm.Messaging;
using MVVMToolkitExtensions.WPF.Models;
using System.Windows;
using System.Windows.Controls;
using MVVMToolkitExtensions.WPF.Interfaces;

namespace MVVMToolkitExtensions.WPF.Controls;

/// <summary>
/// Represents a control that provides a region for view-based navigation.
/// This control allows you to define a region in XAML and then use
/// the <see cref="IRegionManager"/> to place views into it.
/// </summary>
/// <remarks>
/// By defining a <see cref="RegionName"/>, you're effectively creating a placeholder
/// or "region" where content can be injected dynamically using the <see cref="IRegionManager"/>.
/// </remarks>
public class RegionControl : ContentControl, IRegionControl
{
    public static readonly DependencyProperty RegionNameProperty = DependencyProperty.Register(
        nameof(RegionName),
        typeof(string),
        typeof(RegionControl),
        new(null, OnRegionNameChanged)
    );

    public string RegionName
    {
        get => (string)GetValue(RegionNameProperty);
        set => SetValue(RegionNameProperty, value);
    }

    public IEnumerable<object> RegionContent()
    {
        yield return Content;
    }

    public void Add(FrameworkElement view)
    {
        Content = view;
    }

    public void Remove(FrameworkElement view)
    {
        Content = null;
    }

    public void Clear()
    {
        Content = null;
    }

    private static void OnRegionNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if(d is not RegionControl navigationContainer || e.NewValue is not string regionName) return;
        WeakReferenceMessenger.Default.Send(new RegisterRegionMessage((regionName, navigationContainer)));
    }
}
