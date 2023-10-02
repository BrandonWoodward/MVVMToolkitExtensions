using MVVMToolkitExtensions.MAUI.Interfaces;

namespace MVVMToolkitExtensions.MAUI.Factories;

internal sealed class PageFactory : IPageFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IViewRegistry _viewRegistry;

    public PageFactory(IServiceProvider serviceProvider, IViewRegistry viewRegistry)
    {
        _serviceProvider = serviceProvider;
        _viewRegistry = viewRegistry;
    }

    public (TPage Page, object ViewModel) Create<TPage>()
        where TPage : Page
    {
        var (view, viewModel) = Create(typeof(TPage));
        return ((TPage)view, viewModel);
    }

    public (Page View, object ViewModel) Create(Type pageType)
    {
        var viewModelType = _viewRegistry[pageType];
        var view = (Page)_serviceProvider.GetRequiredService(pageType);
        var viewModel = _serviceProvider.GetRequiredService(viewModelType);
        view.BindingContext = viewModel;
        return (view, viewModel);
    }
}