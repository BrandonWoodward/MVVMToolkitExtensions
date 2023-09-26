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
    /// Registers a view and its associated view model to the DI container as transient.
    /// Adds the mapping to the view registry to allow the view to be resolved with the BindingContext set to the ViewModel provided.
    /// </summary>
    /// <typeparam name="TView">The type of the view to register.</typeparam>
    /// <typeparam name="TViewModel">The type of the view model associated with the view.</typeparam>
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
