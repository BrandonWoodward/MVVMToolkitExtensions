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

MVVMToolkitExtensions is designed with IoC in mind and stands on the shoulders of the reputable `Microsoft.Extensions.DependencyInjection` library. Say goodbye to the old ways of setting the `BindingContext` for each view or injecting view models in code-behind; with MVVMToolkitExtensions, enjoy a unified and centralized solution.
&nbsp;

### Setting Up the Application Container:
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

## 3. Expanding Your App
&nbsp;

Once the setup is complete, you're primed to build upon your application. Leverage the [`IRegionManager`](region-manager.md) for region-based navigation or modular view composition. Or, harness the might of the [`INavigationService`](navigation-service.md) for a dynamic, URI-driven, page-focused navigation.

Remember, when using either of these stellar services, the `BindingContext` is set for you, effortlessly.
&nbsp;

---
```

I've interspersed the content with `&nbsp;` to provide additional spacing. Depending on the exact rendering on docfx, you might need to adjust the number of `&nbsp;` to achieve the desired look.