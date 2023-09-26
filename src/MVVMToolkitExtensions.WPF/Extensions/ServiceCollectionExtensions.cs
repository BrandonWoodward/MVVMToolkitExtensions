using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MVVMToolkitExtensions.Core.Interfaces;
using MVVMToolkitExtensions.Core.Models;
using MVVMToolkitExtensions.WPF.Controls;
using MVVMToolkitExtensions.WPF.Factories;
using MVVMToolkitExtensions.WPF.Interfaces;
using MVVMToolkitExtensions.WPF.Services;

namespace MVVMToolkitExtensions.WPF.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers a transient view and its associated view model to the DI container.
    /// </summary>
    /// <typeparam name="TView">The type of the view to register.</typeparam>
    /// <typeparam name="TViewModel">The type of the view model to register.</typeparam>
    /// <param name="services">The IServiceCollection to add the services to.</param>
    /// <returns>The same service collection so that multiple calls can be chained.</returns>
    public static IServiceCollection AddView<TView, TViewModel>(this IServiceCollection services)
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
        services.TryAddSingleton<IRegionRegistry<RegionControl>, RegionRegistry<RegionControl>>();
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
