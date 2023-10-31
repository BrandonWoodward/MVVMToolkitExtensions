using CommunityToolkit.Mvvm.Messaging;
using MVVMToolkitExtensions.MAUI.Models;

namespace MVVMToolkitExtensions.MAUI.Controls;

/// <summary>
/// Represents a control that provides a region for view-based navigation.
/// This control allows you to define a region in XAML and then use a navigation 
/// service to place views into it.
/// </summary>
/// <remarks>
/// By defining a <see cref="RegionName"/>, you're effectively creating a placeholder
/// or "region" where content can be injected dynamically using the navigation service.
/// </remarks>
public class RegionControl : ContentView
{
    private string _regionName;
    public string RegionName
    {
        get => _regionName;
        set
        {
            if (_regionName == value) return;
            _regionName = value;
            WeakReferenceMessenger.Default.Send(new RegisterRegionMessage((_regionName, this)));
        }
    }
}
