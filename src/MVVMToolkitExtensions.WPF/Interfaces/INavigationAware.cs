namespace MVVMToolkitExtensions.WPF.Interfaces;

/// <summary>
/// Tap into the navigation lifecycle of the view
/// </summary>
public interface INavigationAware
{
    /// <summary>
    /// Called when the view is added to the region
    /// </summary>
    /// <param name="parameters">Key/Value pairs to pass to the ViewModel</param>
    void OnNavigatedTo(IParameters parameters);
    
    /// <summary>
    /// Called when the view is removed from the region
    /// Or replaced by another view
    /// </summary>
    void OnNavigatedFrom();
}
