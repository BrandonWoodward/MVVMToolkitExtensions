﻿using MVVMToolkitExtensions.MAUI.Interfaces;
using MVVMToolkitExtensions.MAUI.Models;

namespace MVVMToolkitExtensions.MAUI.Services;

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
        where TView : View
    {
        CheckRegionExists(regionName)
            .HandleNavigationAware(regionName, vm => vm.OnNavigatedFrom())
            .ClearContent(regionName)
            .SetContent<TView>(regionName)
            .HandleNavigationAware(regionName, vm => vm.OnNavigatedTo(parameters ?? NavigationParameters.Empty));
    }

    public void Clear(string regionName)
    {
        CheckRegionExists(regionName)
            .HandleNavigationAware(regionName, vm => vm.OnNavigatedFrom())
            .ClearContent(regionName);
    }

    private RegionManager CheckRegionExists(string regionName)
    {
        if(!_regionRegistry.Contains(regionName))
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
        if(_regionRegistry[regionName].Content is { BindingContext: INavigationAware navigationAwareViewModel })
        {
            action(navigationAwareViewModel);
        }
        return this;
    }

    private RegionManager SetContent<TView>(string regionName)
        where TView : View
    {
        var (view, _) = _viewFactory.Create<TView>();
        _regionRegistry[regionName].Content = view;
        return this;
    }
}

