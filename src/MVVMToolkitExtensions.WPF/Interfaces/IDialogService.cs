using System.Windows;
using MVVMToolkitExtensions.WPF.Models;

namespace MVVMToolkitExtensions.WPF.Interfaces;

/// <summary>
/// Service used to show dialogs and pass parameters to their ViewModels.
/// </summary>
public interface IDialogService
{
    /// <summary>
    /// Show the view provided as a dialog. If your view is a Window, it will be shown as is;
    /// anything else will be wrapped in a Window control.
    /// </summary>
    /// <remarks>
    /// The view must be registered in the DI container using AddViewModelMapping.
    /// </remarks>
    /// <param name="parameters">Optional parameters to pass parameters to the dialog's ViewModel.
    /// Use <see cref="IDialogAware"/> to retrieve the parameters through the OnDialogOpened.
    /// </param>
    /// <typeparam name="TView">The view to show as a dialog.</typeparam>
    void ShowDialog<TView>(DialogParameters? parameters = null) where TView : FrameworkElement;
}
