using MVVMToolkitExtensions.MAUI.Models;

namespace MVVMToolkitExtensions.MAUI.Interfaces;

/// <summary>
/// Provides a contract for services that handle navigation within an application.
/// </summary>
/// <remarks>
/// This interface abstracts the underlying navigation logic, allowing for more modular 
/// and testable code. By implementing this interface, you can define custom navigation 
/// behaviors, while keeping the navigation API consistent across your application.
/// </remarks>
public interface INavigationService
{
    /// <summary>
    /// Asynchronously navigates to the specified URI.
    /// </summary>
    /// <param name="uri">The URI to navigate to. This is typically a route or page identifier.</param>
    /// <param name="parameters">Optional parameters to pass along with the navigation request. 
    /// These parameters can be retrieved in the destination page or view using the 
    /// <see cref="INavigationAware.OnNavigatedTo(NavigationParameters)"/> method.</param>
    /// <returns>A task that represents the asynchronous navigation operation.</returns>
    /// <remarks>
    /// Implementations of this method should handle the complete navigation logic, 
    /// including resolving views, passing parameters, and managing the navigation stack.
    /// </remarks>
    Task NavigateAsync(string uri, NavigationParameters parameters = null);
}