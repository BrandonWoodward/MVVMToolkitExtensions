using Microsoft.Extensions.DependencyInjection.Extensions;
using MVVMToolkitExtensions.MAUI.Factories;
using MVVMToolkitExtensions.MAUI.Interfaces;
using MVVMToolkitExtensions.MAUI.Models;
using MVVMToolkitExtensions.MAUI.Services;

namespace MVVMToolkitExtensions.MAUI.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Extends the <see cref="IServiceCollection"/> to provide registration of a view 
    /// and its associated view model. This method ensures that the view and view model 
    /// are registered to the Dependency Injection (DI) container as transient services.
    /// Furthermore, the method adds the mapping of the view and its view model to the 
    /// view registry, enabling the view to be resolved with its BindingContext
    /// set to the provided ViewModel.
    /// </summary>
    /// <typeparam name="TView">The type of the view to register.</typeparam>
    /// <typeparam name="TViewModel">The type of the view model to associate with the view.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <returns>The same service collection so that multiple calls can be chained.</returns>
    public static IServiceCollection AddViewModelMapping<TView, TViewModel>(this IServiceCollection services)
        where TView : VisualElement
        where TViewModel : class
    {
        return services
            .CheckRequiredServicesRegistered()
            .RegisterViewAndViewModel<TView, TViewModel>()
            .RegisterWithViewRegistry<TView, TViewModel>();
    }

    /// <summary>
    /// Extends the <see cref="IServiceCollection"/> to provide registration of a page for navigation.
    /// The URI route for navigation is inferred from the page type name.
    /// </summary>
    /// <remarks>
    /// It's essential to register the page and its associated view model with the DI container 
    /// using the <see cref="AddViewModelMapping{TView,TViewModel}"/> method before utilizing this method for navigation registration.
    /// </remarks>
    /// <typeparam name="TPage">The type of the page to register.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    public static void AddNavigationRoute<TPage>(this IServiceCollection services)
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
        services.TryAddSingleton<IRegionRegistry, RegionRegistry>();
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
