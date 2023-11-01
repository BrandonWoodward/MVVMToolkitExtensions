using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.Messaging;
using MVVMToolkitExtensions.WPF.Interfaces;
using MVVMToolkitExtensions.WPF.Models;

namespace MVVMToolkitExtensions.WPF.Controls;

/// <summary>
/// Represents a control that provides a region for view-based navigation.
/// This control allows you to define a region in XAML and then use
/// the <see cref="IRegionManager"/> to place views into it. The difference
/// with this control is that the container effectively acts as an ItemsControl,
/// allowing you to define a DataTemplate for the content.
/// </summary>
/// <remarks>
/// By defining a <see cref="RegionName"/>, you're effectively creating a placeholder
/// or "region" where content can be injected dynamically using the <see cref="IRegionManager"/>.
/// </remarks>
public class RegionItemsControl : ItemsControl, IRegionControl
{
    public static readonly DependencyProperty RegionNameProperty = DependencyProperty.Register(
        nameof(RegionName),
        typeof(string),
        typeof(RegionItemsControl),
        new(null, OnRegionNameChanged)
    );

    public string RegionName
    {
        get => (string)GetValue(RegionNameProperty);
        set => SetValue(RegionNameProperty, value);
    }

    private static void OnRegionNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not RegionItemsControl regionItemsControl || e.NewValue is not string regionName) return;
        WeakReferenceMessenger.Default.Send(new RegisterRegionMessage((regionName, regionItemsControl)));
    }
    
    public IEnumerable<object> RegionContent()
    {
        yield return Items;
    }

    public void Add(FrameworkElement view)
    {
        Items.Add(view);
    }

    public void Remove(FrameworkElement view)
    {
        Items.Remove(view);
    }

    public void Clear()
    {
        Items.Clear();
    }
}