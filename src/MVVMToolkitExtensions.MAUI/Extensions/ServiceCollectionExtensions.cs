using Microsoft.Extensions.DependencyInjection.Extensions;
using MVVMToolkitExtensions.Core.Interfaces;
using MVVMToolkitExtensions.Core.Models;
using MVVMToolkitExtensions.MAUI.Controls;
using MVVMToolkitExtensions.MAUI.Factories;
using MVVMToolkitExtensions.MAUI.Interfaces;
using MVVMToolkitExtensions.MAUI.Models;
using MVVMToolkitExtensions.MAUI.Services;

namespace MVVMToolkitExtensions.MAUI.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers a view and its associated view model to the DI container as transient.
    /// </summary>
    /// <typeparam name="TView">The type of the view to register.</typeparam>
    /// <typeparam name="TViewModel">The type of the view model to register.</typeparam>
    /// <param name="services">The IServiceCollection to add the services to.</param>
    /// <returns>The same service collection so that multiple calls can be chained.</returns>
    public static IServiceCollection AddView<TView, TViewModel>(this IServiceCollection services)
        where TView : VisualElement
        where TViewModel : class
    {
        return services
            .CheckRequiredServicesRegistered()
            .RegisterViewAndViewModel<TView, TViewModel>()
            .RegisterWithViewRegistry<TView, TViewModel>();
    }
    
    /// <summary>
    /// Registers a page for navigation. The URI route is inferred from the page type name.
    /// </summary>
    /// <typeparam name="TPage">The type of the page to register.</typeparam>
    public static void AddRoute<TPage>(this IServiceCollection services)
        where TPage : Page
    {
        services.TryAddEnumerable(ServiceDescriptor.Transient(
            typeof(INavigationRegistration),
            typeof(NavigationRegistration<TPage>)
        ));
    }
    
    private static IServiceCollection CheckRequiredServicesRegistered(this IServiceCollection services)
    {
        services.TryAddSingleton<IViewRegistry, ViewRegistry>();
        services.TryAddSingleton<INavigationRegistry, NavigationRegistry>();
        services.TryAddSingleton<IRegionRegistry<RegionControl>, RegionRegistry<RegionControl>>();
        services.TryAddSingleton<IViewFactory, ViewFactory>();
        services.TryAddSingleton<IPageFactory, PageFactory>();
        services.TryAddSingleton<INavigationRoot, NavigationRoot>();
        services.TryAddSingleton<INavigationService, NavigationService>();
        return services;
    }

    private static IServiceCollection RegisterViewAndViewModel<TView, TViewModel>(this IServiceCollection services)
        where TView : VisualElement
        where TViewModel : class
    {
        return services
            .AddTransient<TView>()
            .AddTransient<TViewModel>();
    }

    private static IServiceCollection RegisterWithViewRegistry<TView, TViewModel>(this IServiceCollection services)
        where TView : VisualElement
        where TViewModel : class
    {
        services.TryAddEnumerable(ServiceDescriptor.Transient(
            typeof(IViewRegistration),
            typeof(ViewRegistration<TView, TViewModel, VisualElement>)
        ));
        return services;
    }
}
