using System.Windows;
using MVVMToolkitExtensions.WPF.Controls;
using MVVMToolkitExtensions.WPF.Interfaces;
using MVVMToolkitExtensions.WPF.Models;

namespace MVVMToolkitExtensions.WPF.Services;

internal sealed class RegionManager : IRegionManager
{
    private readonly IViewFactory _viewFactory;
    private readonly IRegionRegistry<RegionControl> _regionRegistry;

    public RegionManager(IViewFactory viewFactory, IRegionRegistry<RegionControl> regionRegistry)
    {
        _viewFactory = viewFactory;
        _regionRegistry = regionRegistry;
    }

    public void Navigate<TView>(string regionName, NavigationParameters? parameters = null) 
        where TView : FrameworkElement
    {
        CheckRegionExists(regionName)
            .ClearContent(regionName)
            .HandleNavigationAware(regionName, vm => vm.OnNavigatedFrom())
            .SetContent<TView>(regionName)
            .HandleNavigationAware(regionName, vm => vm.OnNavigatedTo(parameters ?? NavigationParameters.Empty));
    }

    public void Clear(string regionName) 
    {
        CheckRegionExists(regionName)
            .ClearContent(regionName)
            .HandleNavigationAware(regionName, vm => vm.OnNavigatedFrom());
    }

    private RegionManager CheckRegionExists(string regionName)
    {
        if (!_regionRegistry.Contains(regionName))
            throw new InvalidOperationException($"Region with name {regionName} not found.");
        return this;
    }

    private RegionManager ClearContent(string regionName)
    {
        _regionRegistry[regionName].Content = null;
        return this;
    }

    private RegionManager HandleNavigationAware(string regionName, Action<INavigationAware> action)
    {
        if (_regionRegistry[regionName].Content is FrameworkElement 
               { DataContext: INavigationAware navigationAwareViewModel })
        {
            action(navigationAwareViewModel);
        }
        return this;
    }

    private RegionManager SetContent<TView>(string regionName) 
        where TView : FrameworkElement
    {
        var (view, _) = _viewFactory.Create<TView>();
        _regionRegistry[regionName].Content = view;
        return this;
    }
}

