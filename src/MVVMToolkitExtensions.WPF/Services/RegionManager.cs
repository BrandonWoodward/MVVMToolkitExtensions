using MVVMToolkitExtensions.WPF.Interfaces;
using MVVMToolkitExtensions.WPF.Models;
using System.Windows;

namespace MVVMToolkitExtensions.WPF.Services;

internal sealed class RegionManager : IRegionManager
{
    private readonly IViewFactory _viewFactory;
    private readonly IRegionRegistry _regionRegistry;

    public RegionManager(IViewFactory viewFactory, IRegionRegistry regionRegistry)
    {
        _viewFactory = viewFactory;
        _regionRegistry = regionRegistry;
    }

    public void Navigate<TView>(string regionName, NavigationParameters? parameters = null)
        where TView : FrameworkElement
    {
        HandleNavigationAware(regionName, vm => vm.OnNavigatedFrom())
            .ClearContent(regionName)
            .SetContent<TView>(regionName)
            .HandleNavigationAware(regionName, vm => vm.OnNavigatedTo(parameters ?? NavigationParameters.Empty));
    }

    public void Clear(string regionName)
    {
        ClearContent(regionName)
            .HandleNavigationAware(regionName, vm => vm.OnNavigatedFrom());
    }

    private RegionManager ClearContent(string regionName)
    {
        _regionRegistry[regionName].Clear();
        return this;
    }

    private RegionManager HandleNavigationAware(string regionName, Action<INavigationAware> action)
    {
        if(_regionRegistry[regionName].DataContext is INavigationAware vm) action(vm);
        return this;
    }

    private RegionManager SetContent<TView>(string regionName)
        where TView : FrameworkElement
    {
        var (view, _) = _viewFactory.Create<TView>();
        _regionRegistry[regionName].Add(view);
        return this;
    }
}

