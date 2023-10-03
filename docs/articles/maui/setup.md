# MAUI Setup
&nbsp;

---

## 1. Installation
&nbsp;

Install **MVVMToolkitExtensions.MAUI** through your method of choice:

&nbsp;
- **.NET CLI**:
  
  ```bash
  dotnet add package MVVMToolkitExtensions.MAUI
  ```

&nbsp;
- **Package Manager**:

  ```bash
  Install-Package MVVMToolkitExtensions.MAUI
  ```

&nbsp;

---

## 2. Configuring Dependency Injection

&nbsp;

MVVMToolkitExtensions is designed with IoC in mind and uses the reputable `Microsoft.Extensions.DependencyInjection` library.
Instead of manually setting the `BindingContext` in each view, you can use a centralized approach:

&nbsp;

- **MauiProgram.cs**:

  ```csharp
  public static MauiApp CreateMauiApp()
  {
      var builder = MauiApp.CreateBuilder();

      // Register services and view models
      builder.Services.AddViewModelMapping<MainPage, MainPageViewModel>();
      builder.Services.AddViewModelMapping<LoginPage, LoginPageViewModel>();

      return builder
          .UseMauiApp<App>()
          .WithMainPage<LoginPage>()
          .Build();
  }
  ```

&nbsp;

- **App.xaml.cs**:

  ```csharp
  public partial class App : Application
  {
      public App(Func<Page> loginPage)
      {
          InitializeComponent();
          MainPage = loginPage();
      }
  }
  ```

&nbsp;

---