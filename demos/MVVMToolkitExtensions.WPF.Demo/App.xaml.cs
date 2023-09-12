using AutoWireViewModel.Demos.WPF.ViewModels;
using AutoWireViewModel.Demos.WPF.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using MVVMToolkitExtensions.WPF.Extensions;
using MVVMToolkitExtensions.WPF.Interfaces;

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

        // Register views
        services.AddView<MainWindow, MainWindowViewModel>();
        services.AddView<ViewA, ViewAViewModel>();
        services.AddView<ViewB, ViewBViewModel>();
        services.AddView<DialogA, DialogAViewModel>();
        services.AddView<DialogB, DialogBViewModel>();

        services
            .BuildServiceProvider()
            .Bootstrap<MainWindow>();
    }
}
