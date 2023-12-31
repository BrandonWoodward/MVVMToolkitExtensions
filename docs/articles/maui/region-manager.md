# Region Manager

&nbsp;

---

## 1. Introduction
&nbsp;

The `IRegionManager` provides functionality for dynamic view composition and region-based navigation. 
If you've used Prism before, this will be familiar.
&nbsp;

---

## 2. Basic Usage

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

- **YourViewModel.cs**:

  ```csharp
  public YourViewModel(IRegionManager regionManager)
  {
      _regionManager = regionManager;
  }
  ```

  ```csharp
  _regionManager.Navigate<AnotherView>("AnotherViewRegion");
  ```
&nbsp;

> [!NOTE]
> Ensure that the destination view has been appropriately registered with the DI container.

&nbsp;

---

## 3. Passing Parameters

&nbsp;

Pass parameters to your ViewModel using the `INavigationAware` interface.

&nbsp;

- **YourViewModel.cs**:

  ```csharp
  var parameters = new NavigationParameters {{ "message", "Hello from the MainPage!" }};
  _regionManager.Navigate<AnotherView>("AnotherViewRegion", parameters);
  ```

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
  
&nbsp;

> [!NOTE]
> `OnNavigatedFrom()` is called when the view in the region is replaced
> or the region is cleared by calling `IRegionManager.Clear(regionName)`

---