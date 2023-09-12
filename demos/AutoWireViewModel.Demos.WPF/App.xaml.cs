using AutoWireViewModel.Demos.WPF.Views;
using AutoWireViewModel.WPF;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Windows;

namespace AutoWireViewModel.Demos.WPF;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var services = new ServiceCollection();

        services.AddSingleton<ViewModelLocator>();
        // Add other services here...

        // Register ViewModels from the current assembly
        var serviceProvider = services
            .RegisterViewModels(Assembly.GetExecutingAssembly())
            .BuildServiceProvider();

        // Resolve and initialize the ViewModelLocator
        serviceProvider.GetRequiredService<ViewModelLocator>();

        // Create and show the main window
        var mainWindow = new MainWindow();
        mainWindow.Show();
    }
}
