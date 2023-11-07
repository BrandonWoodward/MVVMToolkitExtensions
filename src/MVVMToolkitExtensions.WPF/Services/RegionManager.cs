using MVVMToolkitExtensions.WPF.Interfaces;
using MVVMToolkitExtensions.WPF.Models;
using System.Windows;

namespace MVVMToolkitExtensions.WPF.Services;

internal sealed class RegionManager : IRegionManager
{
    private readonly IViewFactory _viewFactory;
    private readonly IRegionRegistry _regionRegistry;
    
    public IEnumerable<object> this[string regionName] 
        => _regionRegistry[regionName].RegionContent();

    public RegionManager(IViewFactory viewFactory, IRegionRegistry regionRegistry)
    {
        _viewFactory = viewFactory;
        _regionRegistry = regionRegistry;
    }

    public void Navigate<TView>(string regionName, NavigationParameters? parameters = null)
        where TView : FrameworkElement
    {
        if (_regionRegistry[regionName].RegionContent().Count() == 1)
        {
            var existingView = _regionRegistry[regionName].RegionContent().First();
            if(existingView is FrameworkElement { DataContext: INavigationAware viewModel })
            {
                viewModel.OnNavigatedFrom();
            }
        }
                    
        var (newView, newViewModel) = _viewFactory.Create<TView>();
        _regionRegistry[regionName].Add(newView);

        if (newViewModel is INavigationAware vm)
        {
            vm.OnNavigatedTo(parameters ?? NavigationParameters.Empty);
        }
    }

    public void Clear(string regionName)
    {
        foreach(var view in _regionRegistry[regionName].RegionContent())
        {
            if(view is FrameworkElement { DataContext: INavigationAware viewModel })
            {
                viewModel.OnNavigatedFrom();
            }
        }
        _regionRegistry[regionName].Clear();
    }
}

