# MAUI Quickstart

## Installation

.NET CLI:

```bash
dotnet add package MVVMToolkitExtensions.MAUI
```

Package Manager:

```bash
Install-Package MVVMToolkitExtensions.MAUI
```


## Setup

In your `MauiProgram.cs`, add the following:

```csharp
public static MauiApp CreateMauiApp()
{
    var builder = MauiApp.CreateBuilder();

    // Add services as normal

    // Views and ViewModels
    builder.Services.AddView<MainPage, MainPageViewModel>();
    builder.Services.AddView<LoginPage, LoginPageViewModel>();

    // Routes for navigation
    builder.Services.AddRoute<LoginPage>();

    return builder
        .UseMauiApp<App>()
        .WithMainPage<LoginPage>()
        .Build();
}
```
In your `App.xaml.cs`, add the following to resolve your startup page:

```csharp
public partial class App : Application
{
    public App(Func<Page> mainPage)
    {
        InitializeComponent();
        MainPage = mainPage();
    }
}
```

<br>

## View-Based Navigation

You can programatically add views to regions using the `INavigationService`.
This is similar to the `IRegionManager` in Prism.

### MauiProgram.cs

```csharp
// Make sure to register the view you want to navigate to
services.AddView<AnotherView, AnotherViewModel>();
```

### MainWindow.xaml

```xaml
<regions:RegionControl RegionName="ViewARegion" />
```

### MainWindowViewModel.cs

```csharp
// Ask for the IRegionManager in the constructor
public MainWindowViewModel(IRegionManager regionManager)
{
    _regionManager = regionManager;
}
```

```csharp
// Optionally, you can pass parameters to the view model
// Any object can be passed as a parameter
var parameters = new NavigationParameters 
{ 
    { "message", "Any object here" } 
};

// Creates an instance of the View/ViewModel
// Sets the view as the content of the region
// DataContext is set automatically
_regionManager.Navigate<ViewA>("ViewARegion", parameters);
```

### AnotherViewModel.cs

```csharp
// Implement INavigationAware to receive parameters
// And tap into the navigation lifecycle
public class ViewBViewModel : INavigationAware 
{
    // Will throw if parameter is not found
    public void OnNavigatedTo(NavigationParameters parameters) 
        => Message = parameters.Get<string>("message");
    
    public void OnNavigatedFrom() 
        => Console.WriteLine("Navigated away from ViewB.");
}
```

<br>

## Page-Based Navigation

Page-based navigation offers a robust URI-based system that's seamlessly integrated with the MVVM design pattern. This system supports both absolute and relative navigation paths, ensuring flexibility in addressing different navigation requirements.

### Key Features:
- **URI-Based System**: Navigate using structured URIs to represent pages and their hierarchies.
- **MVVM Friendly**: Easily integrate with MVVM design patterns.
- **Shared Parameter System**: Use the `INavigationAware` interface, ensuring consistency with the view-based navigation.

### Setup and Registration

Before utilizing page-based navigation, you must register your views and view models. 
This registration ensures that the navigation system recognizes the pages and can route to them correctly. 
Register your pages in the `MauiProgram.cs`:

```csharp
// Register Views and their corresponding ViewModels
builder.Services.AddView<MainPage, MainPageViewModel>();
builder.Services.AddView<LoginPage, LoginPageViewModel>();
builder.Services.AddView<PageA, PageAViewModel>();
builder.Services.AddView<PageB, PageBViewModel>();

// Define routes for navigation
builder.Services.AddRoute<MainPage>();
builder.Services.AddRoute<PageA>();
builder.Services.AddRoute<PageB>();
```


Once registered, use the `INavigationService` to navigate between pages. Additionally, this service allows you to pass parameters to the target view model, 
making it versatile for various use-cases:

```csharp
// Ask for the INavigationService in the constructor
public LoginPageViewModel(INavigationService navigationService)
{
	_navigationService = navigationService;
}
```

```csharp
// Optional parameters
var parameters = new NavigationParameters 
{ 
    { "message", "Any object here" } 
};

_navigationService.Navigate("/ViewA", parameters);
```

## URI Navigation Syntax

The `NavigationService` offers two navigation methods: **Relative** and **Absolute**.

---

### Relative Navigation

Relative navigation, as its name suggests, navigates with respect to the current position in the navigation stack. This means you're either pushing new pages onto the stack or moving up and down within the existing stack.

Here's a breakdown of the different patterns you can use:

| Description | Pattern | Action |
|-------------|---------|--------|
| Push a new page to the stack | `"/PageA"` | Pushes `PageA` onto the navigation stack |
| Push multiple new pages to the stack | `"/PageA/PageB"` | Sequentially pushes `PageA` and then `PageB` onto the stack |
| Navigate up one and then to a new page | `"../PageB"` | Pops the current page off the stack, then pushes `PageB` |
| Navigate up two and then to a new page | `"../../PageA"` | Pops two pages off the stack, then pushes `PageA` |
| Navigate to the root page | `"//"` | Pops all pages until reaching the root |

### Absolute Navigation

Absolute navigation resets parts or the entirety of the navigation stack, then navigates to the specified page or pages. It provides a way to start fresh or set a new base for subsequent relative navigation.

Here's a breakdown of the different patterns you can use:

| Description | Pattern | Action |
|-------------|---------|--------|
| Navigate to a new page, but keep the root | `"PageA"` | Clears the stack but retains the root page. Then, pushes `PageA` |
| Navigate to new pages, but keep the root | `"PageA/PageB"` | Clears the stack but retains the root page. Then, pushes `PageA` followed by `PageB` |
| Set a new root and navigate to subsequent pages | `"//PageA/PageB"` | Clears the entire stack, including the root. `PageA` becomes the new root, and then `PageB` is pushed |
