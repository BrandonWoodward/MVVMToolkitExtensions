using System.Windows;
using MVVMToolkitExtensions.WPF.Interfaces;
using MVVMToolkitExtensions.WPF.Models;

namespace MVVMToolkitExtensions.WPF.Services;

internal sealed class NavigationService : INavigationService
{
    private readonly IViewFactory _viewFactory;
    private readonly INavigationRegistry _regionRegistry;

    public NavigationService(IViewFactory viewFactory, INavigationRegistry regionRegistry)
    {
        _viewFactory = viewFactory;
        _regionRegistry = regionRegistry;
    }

    public void Navigate<TView>(string regionName, NavigationParameters parameters) 
        where TView : FrameworkElement
    {
        CheckRegionExists(regionName)
            .ClearContent(regionName)
            .HandleNavigationAware(regionName, vm => vm.OnNavigatedFrom())
            .SetContent<TView>(regionName, parameters)
            .HandleNavigationAware(regionName, vm => vm.OnNavigatedTo(parameters));
    }

    public void Clear(string regionName) 
    {
        CheckRegionExists(regionName)
            .ClearContent(regionName)
            .HandleNavigationAware(regionName, vm => vm.OnNavigatedFrom());
    }

    private NavigationService CheckRegionExists(string regionName)
    {
        if (!_regionRegistry.Contains(regionName))
            throw new InvalidOperationException($"Region with name {regionName} not found.");
        return this;
    }

    private NavigationService ClearContent(string regionName)
    {
        _regionRegistry[regionName].Content = null;
        return this;
    }

    private NavigationService HandleNavigationAware(string regionName, Action<INavigationAware> action)
    {
        if (_regionRegistry[regionName].Content is FrameworkElement 
               { DataContext: INavigationAware navigationAwareViewModel })
        {
            action(navigationAwareViewModel);
        }
        return this;
    }

    private NavigationService SetContent<TView>(string regionName, NavigationParameters parameters) 
        where TView : FrameworkElement
    {
        var (view, _) = _viewFactory.Create<TView>();
        _regionRegistry[regionName].Content = view;
        return this;
    }
}

