<h1 align="center">
  MVVMToolkitExtensions
  <br>
</h1>

<div align="center">

</div>

<h4 align="center"> 
🧰 A simple set of WPF MVVM utilities for use with CommunityToolkit.MVVM and Microsoft.Extensions.DependencyInjection.
</h4>

---

## Installation

Install via the Package Manager Console:

```bash
Install-Package MVVMToolkitUtilities.WPF
```

Or the `dotnet` CLI:

```bash
dotnet add package MVVMToolkitExtensions.WPF
```

## Setup

In your `App.xaml.cs`, add the following:

```csharp
protected override void OnStartup(StartupEventArgs e)
{
    base.OnStartup(e);
    var services = new ServiceCollection();

    // Register services as normal...
    services.AddSingleton<IMyService, MyService>();
    
    // Register views with their view models
    services.AddView<MainWindow, MainWindowViewModel>();
    
    // Create service provider and show provided view
    services
        .BuildServiceProvider()
        .Bootstrap<MainWindow>();
}
```

## Navigation

You can programatically add views to regions using the `INavigationService`.
This is similar to the `IRegionManager` in Prism.

### App.xaml.cs

```csharp
// Make sure to register the view you want to navigate to
services.AddView<AnotherView, AnotherViewModel>();
```

### MainWindow.xaml

```xaml
<regions:NavigationContainer RegionName="ViewARegion" />
```
### MainWindowViewModel.cs

```csharp
// Ask for the INavigationService in the constructor
public MainWindowViewModel(INavigationService navigationService)
{
    _navigationService = navigationService;
}
```

```csharp
// Optionally, you can pass parameters to the view model
// Any object can be passed as a parameter
// Use NavigationParameters.Empty for no parameters
var parameters = new NavigationParameters 
{ 
    { "message", "Any object here" } 
};

// Creates an instance of the View/ViewModel
// Sets the view as the content of the region
// DataContext is set automatically
regionService.NavigateTo<ViewA>("ViewARegion", parameters);
```

### AnotherViewModel.cs

```csharp
// Implement INavigationAware to receive parameters
// And tap into the navigation lifecycle
public class ViewBViewModel : INavigationAware 
{
    // Will throw if parameter is not found
    public void OnNavigatedTo(IParameters parameters) 
        => Message = parameters.Get<string>("message");
    
    public void OnNavigatedFrom() 
        => Console.WriteLine("Navigated away from ViewB.");
}
```

## Dialogs

You can show dialogs using the `IDialogService`. Again, this will feel familiar if you've used Prism.

### App.xaml.cs

```csharp
// Make sure to register the view you want to use for a dialog
services.AddView<DialogA, DialogAViewModel>();
```

### ViewAViewModel.cs

```csharp
// Ask for the IDialogService in the constructor
public SomeViewModel(IDialogService dialogService)
{
    _dialogService = dialogService;
}
```

```csharp
// Optionally, you can pass parameters to the view model
// Any object can be passed as a parameter
// Use DialogParameters.Empty for no parameters
var parameters = new DialogParameters
{
    { "message", "Any object here" },
};

// Creates an instance of the View/ViewModel
// Shows the control as a dialog
// DataContext is set automatically
_dialogService.ShowDialog<DialogA>(parameters);
```

### DialogAViewModel.cs

```csharp
// Implement IDialogAware to receive parameters
// And tap into the dialog lifecycle
public class DialogAViewModel : IDialogAware
{
    public bool CanCloseDialog() 
        => true;

    public void OnDialogClosed() 
        => Debug.WriteLine("Dialog closed.");

    public void OnDialogOpened(IParameters parameters)
        => Message = parameters.Get<string>("message");
}
```