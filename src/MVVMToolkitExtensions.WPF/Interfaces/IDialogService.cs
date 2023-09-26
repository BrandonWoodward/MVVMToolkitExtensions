using System.Windows;
using MVVMToolkitExtensions.Core.Interfaces;

namespace MVVMToolkitExtensions.WPF.Interfaces;

/// <summary>
/// Service used to show dialogs and pass parameters to them.
/// Dialogs must be registered in the DI container with AddView
/// </summary>
public interface IDialogService
{
    void ShowDialog<TView>(IParameters parameters) where TView : FrameworkElement;
}
