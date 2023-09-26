using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using MVVMToolkitExtensions.Core.Interfaces;
using MVVMToolkitExtensions.WPF.Adapters;
using MVVMToolkitExtensions.WPF.Interfaces;

namespace MVVMToolkitExtensions.WPF.Factories;

internal sealed class DialogFactory : IDialogFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IViewRegistry _viewRegistry;

    public DialogFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _viewRegistry = serviceProvider.GetRequiredService<IViewRegistry>();
    }

    public (IDialogWindow View, object ViewModel) Create<TView>() where TView : FrameworkElement
    {
        // Get the corresponding view model type from the registry
        // Resolve the view and view model from the DI container
        // This will create new instances because they are transient
        var viewModelType = _viewRegistry[typeof(TView)];
        var view = _serviceProvider.GetRequiredService<TView>();
        var viewModel = _serviceProvider.GetRequiredService(viewModelType);
        view.DataContext = viewModel;

        // Adapt the control to the IDialogWindow interface
        var dialogView = new DialogWindowAdapter(view);
        return (dialogView, (IDialogAware)viewModel);
    }
}
