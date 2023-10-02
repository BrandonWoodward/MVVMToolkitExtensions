using System.Windows;
using MVVMToolkitExtensions.WPF.Models;

namespace MVVMToolkitExtensions.WPF.Interfaces;

/// <summary>
/// Provides support for view-based navigation using the RegionControl.
/// </summary>
public interface IRegionManager
{
    /// <summary>
    /// Set the content of the region to the specified view. The DataContext will be automatically set.
    /// </summary>
    /// <param name="regionName">The name of the region you provided in your XAML.</param>
    /// <param name="parameters">Optional parameters to pass to the ViewModel. Use <see cref="INavigationAware"/> to
    /// obtain these parameters through the OnNavigatedTo() method.</param>
    /// <typeparam name="TView"></typeparam>
    void Navigate<TView>(string regionName, NavigationParameters? parameters = null) where TView : FrameworkElement;
    
    /// <summary>
    /// Clear the content of the region.
    /// </summary>
    /// <param name="regionName">The name of the region you provided in your XAML.</param>
    void Clear(string regionName);
}