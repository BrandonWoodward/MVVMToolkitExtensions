using MVVMToolkitExtensions.Core.Factories;
using MVVMToolkitExtensions.MAUI.Interfaces;

namespace MVVMToolkitExtensions.MAUI.Factories; 

internal sealed class PageFactory : FactoryBase<Page>, IPageFactory
{
    public PageFactory(IServiceProvider serviceProvider)
        : base(serviceProvider) { }

    public (TPage Page, object ViewModel) Create<TPage>() where TPage : Page
    {
        var (view, viewModel) = CreateCore<TPage>();
        view.BindingContext = viewModel;
        return (view, viewModel);
    }

    public (Page View, object ViewModel) Create(Type pageType)
    {
        var (view, viewModel) = CreateCore(pageType);
        view.BindingContext = viewModel;
        return (view, viewModel);
    }
}