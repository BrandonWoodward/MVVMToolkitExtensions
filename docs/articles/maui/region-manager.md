# Region Manager
&nbsp;

---

## 1. Introduction
&nbsp;

The `IRegionManager` provides an avenue for dynamic view composition and region-based navigation that aligns harmoniously with MVVM paradigms. If you're acquainted with the Prism library, you'll find many familiar concepts here.
&nbsp;

---

## 2. Basic Usage
&nbsp;

### Setting up a Region in the View:
&nbsp;

- **YourView.xaml**:

  ```xaml
  <ContentPage
      x:Class="YourNamespace.YourView"
      xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
      xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
      xmlns:region="clr-namespace:MVVMToolkitExtensions.MAUI.Controls;assembly=MVVMToolkitExtensions.MAUI">

      <ContentPage.Content>
          <region:RegionControl RegionName="AnotherViewRegion" />
      </ContentPage.Content>
  </ContentPage>
  ```
&nbsp;

### Using the RegionManager:
&nbsp;

- **YourViewModel.cs**:

  Acquire the `IRegionManager` via constructor dependency injection:

  ```csharp
  public YourViewModel(IRegionManager regionManager)
  {
      _regionManager = regionManager;
  }
  ```

  Proceed to navigate to the designated view:

  ```csharp
  _regionManager.Navigate<AnotherView>("AnotherViewRegion");
  ```
&nbsp;

> [!NOTE]
> Ensure that the destination view has been appropriately registered within the DI container:
> `builder.Services.AddViewModelMapping<AnotherView, AnotherViewModel>();`
&nbsp;

---

## 3. Passing Parameters
&nbsp;

Pass parameters to your ViewModel using the `INavigationAware` interface.
&nbsp;

### Dispatching Parameters:
&nbsp;

- **YourViewModel.cs**:

  ```csharp
  var parameters = new NavigationParameters {{ "message", "Hello from the MainPage!" }};
  _regionManager.Navigate<AnotherView>("AnotherViewRegion", parameters);
  ```

&nbsp;
### Acquiring Parameters:
&nbsp;

- **AnotherViewModel.cs**:

  ```csharp
  public class AnotherViewModel : INavigationAware
  {
      public void OnNavigatedTo(NavigationParameters parameters)
      {
          var message = parameters.Get<string>("message");
      }

      public void OnNavigatedFrom()
      {
          // Define actions to undertake when navigation departs from this view
      }
  }
  ```

  Note: The `OnNavigatedFrom` function is summoned when the view departs from the region, either due to transition to another view in the same region or via an `IRegionManager.Clear` call.
  &nbsp;

---