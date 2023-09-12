using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using MVVMToolkitExtensions.WPF.Controls;
using MVVMToolkitExtensions.WPF.Interfaces;

namespace MVVMToolkitExtensions.WPF.Extensions;

public static class ServiceProviderExtensions 
{
    /// <summary>
    /// Bootstraps the application by registering some required services and showing the provided window.
    /// </summary>
    /// <typeparam name="TView">The main view type to be displayed after bootstrapping.</typeparam>
    /// <param name="serviceProvider">The service provider from which services are resolved.</param>
    /// <returns>The service provider for potential further chaining.</returns>
    public static IServiceProvider Bootstrap<TView>(this IServiceProvider serviceProvider)
        where TView : Window 
    {
        var navigationRegistry = serviceProvider.GetRequiredService<INavigationRegistry>();
        
        // TODO - Exposing global state like this is not ideal
        // TODO - How else can I access these services in static callback for DependencyProperty?
        Application.Current.Resources["NavigationRegistry"] = navigationRegistry;
        
        // Show the main window
        serviceProvider
            .GetRequiredService<IViewFactory>()
            .Create<TView>()
            .View.Show();

        return serviceProvider;
    }
}