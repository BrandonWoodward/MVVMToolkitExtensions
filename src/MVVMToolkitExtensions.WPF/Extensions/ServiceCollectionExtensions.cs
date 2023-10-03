using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MVVMToolkitExtensions.WPF.Factories;
using MVVMToolkitExtensions.WPF.Interfaces;
using MVVMToolkitExtensions.WPF.Models;
using MVVMToolkitExtensions.WPF.Services;
using System.Windows;

namespace MVVMToolkitExtensions.WPF.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers a view and its associated view model to the DI container with a transient lifetime.
    /// This method also adds a mapping between the view and its view model in the view registry, ensuring that
    /// when the view is resolved, its DataContext (or BindingContext) will be automatically set to an instance of the specified ViewModel.
    /// </summary>
    /// <typeparam name="TView">The type of the view to register. This type must derive from <see cref="FrameworkElement"/>.</typeparam>
    /// <typeparam name="TViewModel">The type of the view model to associate with the specified view. This type should typically implement related interfaces or patterns for data binding and navigation.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the registrations to.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance, allowing for method chaining.</returns>
    /// <remarks>
    /// This abstracts away the complexity of setting up the data context and encourages a clean separation of concerns in MVVM-based applications.
    /// </remarks>
    public static IServiceCollection AddViewModelMapping<TView, TViewModel>(this IServiceCollection services)
        where TView : FrameworkElement
        where TViewModel : class
    {
        return services
            .CheckRequiredServicesRegistered()
            .AddViewAndViewModel<TView, TViewModel>()
            .AddViewAndViewModelMapping<TView, TViewModel>();
    }

    private static IServiceCollection CheckRequiredServicesRegistered(this IServiceCollection services)
    {
        services.TryAddSingleton<IViewRegistry, ViewRegistry>();
        services.TryAddSingleton<IRegionRegistry, RegionRegistry>();
        services.TryAddSingleton<IViewFactory, ViewFactory>();
        services.TryAddSingleton<IDialogFactory, DialogFactory>();
        services.TryAddSingleton<IDialogService, DialogService>();
        services.TryAddSingleton<IRegionManager, RegionManager>();
        return services;
    }

    private static IServiceCollection AddViewAndViewModel<TView, TViewModel>(this IServiceCollection services)
        where TView : FrameworkElement
        where TViewModel : class
    {
        return services
            .AddTransient<TView>()
            .AddTransient<TViewModel>();
    }

    private static IServiceCollection AddViewAndViewModelMapping<TView, TViewModel>(this IServiceCollection services)
        where TView : FrameworkElement
        where TViewModel : class
    {
        services.TryAddEnumerable(ServiceDescriptor.Transient(
            typeof(IViewRegistration),
            typeof(ViewRegistration<TView, TViewModel, FrameworkElement>)
        ));
        return services;
    }
}
