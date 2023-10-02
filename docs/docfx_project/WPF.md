# WPF Quickstart

## Installation

.NET CLI:

```bash
dotnet add package MVVMToolkitExtensions.WPF
```

Package Manager:

```bash
Install-Package MVVMToolkitExtensions.WPF
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
        .WithMainWindow<MainWindow>();
}
```

<br>

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
// Ask for the IRegionManager in the constructor
public MainWindowViewModel(IRegionManager regionManager)
{
    _regionManager = regionManager;
}
```

```csharp
// Optional parameters
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
// Optional parameters
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

    public void OnDialogOpened(DialogParameters parameters)
        => Message = parameters.Get<string>("message");
}
```
