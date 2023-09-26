using Microsoft.Extensions.DependencyInjection;
using MVVMToolkitExtensions.Core.Interfaces;

namespace MVVMToolkitExtensions.Core.Factories;

internal abstract class FactoryBase<TViewType> where TViewType : class
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IViewRegistry _viewRegistry;

    protected FactoryBase(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _viewRegistry = serviceProvider.GetRequiredService<IViewRegistry>();
    }

    protected (TSpecificView View, object ViewModel) CreateCore<TSpecificView>()
        where TSpecificView : TViewType
    {
        var result = CreateCore(typeof(TSpecificView));
        return ((TSpecificView)result.View, result.ViewModel);
    }

    protected (TViewType View, object ViewModel) CreateCore(Type viewType)
    {
        var viewModelType = _viewRegistry[viewType];
        var view = (TViewType)_serviceProvider.GetRequiredService(viewType);
        var viewModel = _serviceProvider.GetRequiredService(viewModelType);
        return (view, viewModel);
    }
}