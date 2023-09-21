using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using MVVMToolkitExtensions.WPF.Interfaces;

namespace MVVMToolkitExtensions.WPF.Factories;

internal sealed class ViewFactory : IViewFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IViewRegistry _viewRegistry;

    public ViewFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _viewRegistry = serviceProvider.GetRequiredService<IViewRegistry>();
    }

    public (TView View, object ViewModel) Create<TView>() 
        where TView : FrameworkElement
    {
        // Find the corresponding view model type from the registry
        // Resolve the view and view model from the DI container
        // This will create new instances because they are transient
        var viewModelType = _viewRegistry[typeof(TView)];
        var view = _serviceProvider.GetRequiredService<TView>();
        var viewModel = _serviceProvider.GetRequiredService(viewModelType);
        view.DataContext = viewModel;
        return (view, viewModel);
    }
}
