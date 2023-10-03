# Navigation Service
&nbsp;

---

## 1. Introduction
&nbsp;

The page-based navigation system provides a robust URI-based navigation mechanism that's in harmony with the MVVM design pattern. With support for both absolute and relative paths, it offers flexibility for diverse navigation needs.
&nbsp;

---

## 2. Basic Usage
&nbsp;

To make use of the page-based navigation, it's essential to register ViewModel mappings for `INavigationService` to correctly set the `BindingContext` of the desired page. Subsequently, pages for navigation must also be registered.
&nbsp;

> [!NOTE]
> All routes must be derived from `Page`.
&nbsp;

- **MauiProgram.cs**:

  ```csharp
  builder.Services.AddViewModelMapping<MainPage, MainPageViewModel>();
  builder.Services.AddViewModelMapping<LoginPage, LoginPageViewModel>();
  builder.Services.AddViewModelMapping<PageA, PageAViewModel>();
  builder.Services.AddViewModelMapping<PageB, PageBViewModel>();

  builder.Services.AddNavigationRoute<MainPage>();
  builder.Services.AddNavigationRoute<PageA>();
  builder.Services.AddNavigationRoute<PageB>();
  ```
&nbsp;

- **LoginPageViewModel.cs**:

  Inject the `INavigationService`:

  ```csharp
  public LoginPageViewModel(INavigationService navigationService)
  {
      _navigationService = navigationService;
  }
  ```

  Navigate using the service:

  ```csharp
  _navigationService.Navigate("/ViewA", parameters);
  ```
&nbsp;

---

## 3. URI Navigation Syntax
&nbsp;

The `NavigationService` supports two primary navigation styles: **Relative** and **Absolute**.
&nbsp;

### Relative Navigation:

| Description                                       | Pattern             | Action                                                                                 |
|---------------------------------------------------|---------------------|----------------------------------------------------------------------------------------|
| Push a new page                                   | `"/PageA"`          | Adds `PageA` to the navigation stack                                                   |
| Push multiple pages                               | `"/PageA/PageB"`    | Adds `PageA` followed by `PageB` to the stack                                          |
| Navigate up one level, then push a new page       | `"../PageB"`        | Removes current page and adds `PageB`                                                  |
| Navigate up two levels, then push a new page      | `"../../PageA"`     | Removes two pages and then adds `PageA`                                                |
| Navigate to the root                              | `"//"`              | Clears all pages, reaching the root of the navigation stack                            |

<br/>

### Absolute Navigation:

| Description                                       | Pattern             | Action                                                                                 |
|---------------------------------------------------|---------------------|----------------------------------------------------------------------------------------|
| Navigate and retain the root                      | `"PageA"`           | Clears the stack, retains the root, and adds `PageA`                                   |
| Navigate to multiple pages and retain the root    | `"PageA/PageB"`     | Clears the stack, retains the root, then sequentially adds `PageA` and `PageB`         |
| Set a new root and navigate to subsequent pages   | `"//PageA/PageB"`   | Clears the entire stack. Sets `PageA` as root, then adds `PageB`                       |

<br/>

---

## 4. Navigating with Parameters

&nbsp;

Parameters can be effortlessly passed to the ViewModel through the `INavigationAware` interface.
&nbsp;

- **LoginPageViewModel.cs**:

  ```csharp
  var parameters = new NavigationParameters {{ "username", Username }};
  _regionManager.Navigate<AnotherView>("AnotherViewRegion", parameters);
  ```
&nbsp;

- **MainPageViewModel.cs**:

  ```csharp
  public class MainPageViewModel : INavigationAware
  {
      public void OnNavigatedTo(NavigationParameters parameters)
      {
          var username = parameters.Get<string>("username");
      }

      public void OnNavigatedFrom()
      {
          // Handle actions when navigation moves away from this page
      }
  }
  ```

  Note: The `OnNavigatedFrom` method is triggered when the page is removed from the navigation stack.

---