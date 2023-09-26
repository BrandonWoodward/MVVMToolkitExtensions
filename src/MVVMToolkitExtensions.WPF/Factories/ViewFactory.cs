using System.Windows;
using MVVMToolkitExtensions.Core.Factories;
using MVVMToolkitExtensions.WPF.Interfaces;

namespace MVVMToolkitExtensions.WPF.Factories;

internal sealed class ViewFactory : FactoryBase<FrameworkElement>, IViewFactory
{
    public ViewFactory(IServiceProvider serviceProvider)
        : base(serviceProvider) { }

    public (TView View, object ViewModel) Create<TView>() 
        where TView : FrameworkElement
    {
        var (view, viewModel) = CreateCore<TView>();
        view.DataContext = viewModel;
        return (view, viewModel);
    }

    public (FrameworkElement View, object ViewModel) Create(Type viewType)
    {
        var (view, viewModel) = CreateCore(viewType);
        view.DataContext = viewModel;
        return (view, viewModel);
    }
}
