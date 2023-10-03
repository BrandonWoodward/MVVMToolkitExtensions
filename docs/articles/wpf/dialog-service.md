# Dialog Service

&nbsp;

---

## 1. Introduction

&nbsp;

The `IDialogService` provides functionality for MVVM-friendly dialogs.
If you've used Prism before, this is very similar.

&nbsp;

---

## 2. Basic Usage

&nbsp;

```csharp
public YourViewModel(IDialogService dialogService)
{
    _dialogService = dialogService;
}
```

```csharp
_dialogService.ShowDialog<YourDialog>();
```

&nbsp;

> [!NOTE]
> Ensure that the dialog has been appropriately registered with the DI container.

&nbsp;

---

## 3. Passing Parameters

&nbsp;

The `IDialogAware` interface exposes 3 methods, `OnDialogOpened`, `OnDialogClosed`, and `CanCloseDialog`, 
which are self-explanatory.

You can use the `OnDialogOpened` method to pass parameters to your dialog.

&nbsp;

- **YourViewModel.cs**:

  ```csharp
  var parameters = new DialogParameters {{ "message", "Hello from the MainPage!" }};
  _dialogService.ShowDialog<AnotherView>("AnotherViewRegion", parameters);
  ```

&nbsp;

- **AnotherViewModel.cs**:

  ```csharp
  public class AnotherViewModel : IDialogAware
  {
      public void OnDialogOpened(DialogParameters parameters)
      {
          var message = parameters.Get<string>("message");
      }

      public void OnDialogClosed()
      {
          // Define actions to undertake when navigation departs from this view
      }
  
      public bool CanCloseDialog()
      {
          // Define conditions for which the dialog can be closed
      }
  }
  ```

&nbsp;

> [!NOTE]
> Any views that derive from `Window` will be shown as-is, 
> anything else will be wrapped in a `Window` control.

---