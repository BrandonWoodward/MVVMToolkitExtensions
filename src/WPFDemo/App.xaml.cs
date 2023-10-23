using System;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MVVMToolkitExtensions.WPF.Extensions;
using WPFDemo.ViewModels;
using WPFDemo.Views;

namespace WPFDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static readonly IHost host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(a =>
            {
                a.SetBasePath(Environment.CurrentDirectory);
            })
            .ConfigureServices((context, services) =>
            {
                services.AddViewModelMapping<MainWindow, MainWindowViewModel>();
            })
            .Build();

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            await host.StartAsync();
            host.Services.WithMainWindow<MainWindow>();
        }
    }
}