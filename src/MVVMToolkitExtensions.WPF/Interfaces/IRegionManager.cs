using MVVMToolkitExtensions.WPF.Models;
using System.Windows;

namespace MVVMToolkitExtensions.WPF.Interfaces;

/// <summary>
/// Provides a contract for services that manage view-based navigation within regions defined by <see cref="RegionControl"/>.
/// </summary>
/// <remarks>
/// The <c>IRegionManager</c> interface is integral to the region-based navigation approach. It offers methods to navigate
/// to a specific view within a named region and to clear the content of a named region. The associated region in XAML
/// is identified by its name using the <see cref="RegionControl"/>.
/// </remarks>
public interface IRegionManager
{
    /// <summary>
    /// Navigates to the specified view and sets it as the content of the specified region. The view's 
    /// <c>BindingContext</c> (ViewModel) will be automatically set.
    /// </summary>
    /// <param name="regionName">The name of the region as defined in XAML using <see cref="RegionControl"/>.</param>
    /// <param name="parameters">Optional parameters to pass to the view's ViewModel. If the ViewModel 
    /// implements <see cref="INavigationAware"/>, these parameters can be retrieved using the 
    /// <see cref="INavigationAware.OnNavigatedTo(NavigationParameters)"/> method.</param>
    /// <typeparam name="TView">The type of the view you wish to navigate to. This should be a subtype of <see cref="View"/>.</typeparam>
    /// <remarks>
    /// This method is used to dynamically change the content of a named region to the specified view. It abstracts the 
    /// complexity of view resolution, ViewModel association, and parameter passing.
    /// </remarks>
    void Navigate<TView>(string regionName, NavigationParameters? parameters = null) where TView : FrameworkElement;

    /// <summary>
    /// Clears the content of the specified region, effectively removing any view currently displayed within it.
    /// This will trigger the <see cref="INavigationAware.OnNavigatedFrom()"/> method.
    /// </summary>
    /// <param name="regionName">The name of the region as defined in XAML using <see cref="RegionControl"/>.</param>
    /// <remarks>
    /// Use this method to reset or clear the content of a region, especially in scenarios where you need to ensure 
    /// that no content is being displayed within a specific region.
    /// </remarks>
    void Clear(string regionName);

    IEnumerable<object> this[string regionName] { get; }
}