using Microsoft.Extensions.DependencyInjection;
using MVVMToolkitExtensions.WPF.Interfaces;
using System.Windows;

namespace MVVMToolkitExtensions.WPF.Extensions;

public static class ServiceProviderExtensions
{
    /// <summary>
    /// Bootstraps the application by registering some required services and showing the provided window.
    /// The DataContext will be set automatically to the ViewModel registered for the provided view.
    /// </summary>
    /// <typeparam name="TView">The main view type to be displayed after bootstrapping.</typeparam>
    public static IServiceProvider WithMainWindow<TView>(this IServiceProvider serviceProvider)
        where TView : Window
    {
        // Show the main window
        serviceProvider
            .GetRequiredService<IViewFactory>()
            .Create<TView>()
            .View.Show();

        return serviceProvider;
    }
}