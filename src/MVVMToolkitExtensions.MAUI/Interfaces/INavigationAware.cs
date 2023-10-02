using MVVMToolkitExtensions.MAUI.Models;

namespace MVVMToolkitExtensions.MAUI.Interfaces;

/// <summary>
/// Provides methods to handle the navigation lifecycle of views or pages.
/// Classes that implement this interface can receive notifications when 
/// they are navigated to or from.
/// </summary>
/// <remarks>
/// Use this interface to execute logic when the view or page is navigated to or from.
/// This allows for dynamic behavior, such as loading or saving state, based on the navigation 
/// flow of the application.
/// </remarks>
public interface INavigationAware
{
    /// <summary>
    /// Called when the associated view or page is navigated to.
    /// </summary>
    /// <param name="parameters">A collection of key/value pairs passed to the view or page, typically used 
    /// to pass data between views or pages during navigation.</param>
    /// <remarks>
    /// Use this method to handle any setup or data initialization logic when the view 
    /// or page becomes the current active view or page in the navigation stack.
    /// </remarks>
    void OnNavigatedTo(NavigationParameters parameters);

    /// <summary>
    /// Called when the associated view or page is navigated away from, typically 
    /// when a new view or page is pushed onto the navigation stack.
    /// </summary>
    /// <remarks>
    /// This method is ideal for cleanup, saving state, or halting ongoing operations 
    /// related to the current view or page.
    /// </remarks>
    void OnNavigatedFrom();
}
