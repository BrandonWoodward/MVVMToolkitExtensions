namespace MVVMToolkitExtensions.MAUI.Interfaces; 

/// <summary>
/// Creates Page and ViewModel pairs with the BindingContext set.
/// </summary>
public interface IPageFactory
{
    /// <summary>
    /// Creates a Page and ViewModel pair with the BindingContext set. The page must be registered using AddView().
    /// </summary>
    /// <typeparam name="TPage">The page to create.</typeparam>
    /// <returns>A tuple containing the View and the corresponding ViewModel.</returns>
    (TPage Page, object ViewModel) Create<TPage>() 
        where TPage : Page;

    /// <summary>
    /// Creates a Page and ViewModel pair with the BindingContext set.
    /// </summary>
    /// <param name="viewType">The page to create.</param>
    /// <returns>A tuple containing the View and the corresponding ViewModel.</returns>
    (Page View, object ViewModel) Create(Type viewType);
}