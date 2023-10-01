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
    /// Adds the mapping to the view registry to allow the view to be resolved with the BindingContext set to the ViewModel provided.
    /// </summary>
    /// <typeparam name="TView">The type of the view to register.</typeparam>
    /// <typeparam name="TViewModel">The type of the view model associated with the view.</typeparam>
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
    /// <remarks>
    /// You should also register the page and it's view model with the DI container using the AddView method.
    /// </remarks>
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
        services.TryAddSingleton<IRegionManager, RegionManager>();
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
