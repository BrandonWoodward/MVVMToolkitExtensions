namespace MVVMToolkitExtensions.MAUI.Interfaces;

/// <summary>
/// Creates View and ViewModel pairs with the BindingContext set.
/// </summary>
public interface IViewFactory
{
    /// <summary>
    /// Creates a Page and ViewModel pair with the BindingContext set.
    /// </summary>
    /// <typeparam name="TView">The view to create.</typeparam>
    /// <returns>A tuple containing the View and the corresponding ViewModel.</returns>
    (TView View, object ViewModel) Create<TView>() 
        where TView : VisualElement;

    /// <summary>
    /// Creates a View and ViewModel pair with the BindingContext set.
    /// </summary>
    /// <param name="viewType">The view to create.</param>
    /// <returns>A tuple containing the View and the corresponding ViewModel.</returns>
    (VisualElement View, object ViewModel) Create(Type viewType);
}