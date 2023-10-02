using MVVMToolkitExtensions.MAUI.Interfaces;

namespace MVVMToolkitExtensions.MAUI.Factories;

internal sealed class ViewFactory : IViewFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IViewRegistry _viewRegistry;

    public ViewFactory(IServiceProvider serviceProvider, IViewRegistry viewRegistry)
    {
        _serviceProvider = serviceProvider;
        _viewRegistry = viewRegistry;
    }

    public (TView View, object ViewModel) Create<TView>()
        where TView : VisualElement
    {
        var (view, viewModel) = Create(typeof(TView));
        return ((TView)view, viewModel);
    }

    public (VisualElement View, object ViewModel) Create(Type pageType)
    {
        var viewModelType = _viewRegistry[pageType];
        var view = (VisualElement)_serviceProvider.GetRequiredService(pageType);
        var viewModel = _serviceProvider.GetRequiredService(viewModelType);
        view.BindingContext = viewModel;
        return (view, viewModel);
    }
}
