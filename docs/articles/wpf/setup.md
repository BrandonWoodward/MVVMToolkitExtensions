# WPF Setup
&nbsp;

---

## 1. Installation
&nbsp;

Install **MVVMToolkitExtensions.WPF** through your method of choice:

&nbsp;
- **.NET CLI**:

  ```bash
  dotnet add package MVVMToolkitExtensions.WPF
  ```

&nbsp;
- **Package Manager**:

  ```bash
  Install-Package MVVMToolkitExtensions.WPF
  ```

&nbsp;

---

## 2. Configuring Dependency Injection

&nbsp;

MVVMToolkitExtensions is designed with IoC in mind and uses the reputable `Microsoft.Extensions.DependencyInjection` library. 
Instead of manually setting the `DataContext` in each view, you can use a centralized approach:

&nbsp;

- **App.xaml.cs**:

  ```csharp
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var services = new ServiceCollection();

        // Register services and viewmodels
        services.AddViewModelMapping<MainWindow, MainWindowViewModel>();
        services.AddViewModelMapping<ViewA, ViewAViewModel>();

        // Show the main window
        services
            .BuildServiceProvider()
            .WithMainWindow<MainWindow>();
    }
  ```
&nbsp;