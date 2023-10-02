using Microsoft.Extensions.DependencyInjection;
using MVVMToolkitExtensions.WPF.Interfaces;
using System.Windows;

namespace MVVMToolkitExtensions.WPF.Extensions;

public static class ServiceProviderExtensions
{
    /// <summary>
    /// Configures the application to display the specified main window upon startup. The <c>DataContext</c> 
    /// of the window will be automatically set to the ViewModel that's registered for the provided view type.
    /// </summary>
    /// <typeparam name="TView">The type of the main window that should be displayed. This type must derive from <see cref="Window"/>.</typeparam>
    /// <param name="serviceProvider">The service provider that holds registrations for the application's services.</param>
    /// <returns>The original service provider to allow for method chaining.</returns>
    /// <remarks>
    /// This method is typically called during application startup, after the service provider has been configured 
    /// with all required services. It ensures that the main window of the application is displayed, 
    /// and that its data context is correctly set up.
    /// </remarks>
    public static IServiceProvider WithMainWindow<TView>(this IServiceProvider serviceProvider)
        where TView : Window
    {

        serviceProvider.GetRequiredService<IViewRegistry>();
        serviceProvider.GetRequiredService<IRegionRegistry>();

        // Show the main window
        serviceProvider
            .GetRequiredService<IViewFactory>()
            .Create<TView>()
            .View.Show();

        return serviceProvider;
    }
}