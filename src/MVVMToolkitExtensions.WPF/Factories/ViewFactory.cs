using Microsoft.Extensions.DependencyInjection;
using MVVMToolkitExtensions.WPF.Interfaces;
using System.Windows;

namespace MVVMToolkitExtensions.WPF.Factories;

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
        where TView : FrameworkElement
    {
        var (view, viewModel) = Create(typeof(TView));
        return ((TView)view, viewModel);
    }

    public (FrameworkElement View, object ViewModel) Create(Type pageType)
    {
        var viewModelType = _viewRegistry[pageType];
        var view = (FrameworkElement)_serviceProvider.GetRequiredService(pageType);
        var viewModel = _serviceProvider.GetRequiredService(viewModelType);
        view.DataContext = viewModel;
        return (view, viewModel);
    }
}
