using MVVMToolkitExtensions.WPF.Models;

namespace MVVMToolkitExtensions.WPF.Interfaces;

/// <summary>
/// Provides methods to handle the lifecycle of a dialog window. Implementing this 
/// interface allows a ViewModel to control and respond to the opening and closing events of its associated dialog.
/// </summary>
/// <remarks>
/// Use this interface to manage the behavior and state of dialog windows within the application. 
/// This ensures that any necessary setup or cleanup actions are executed when the dialog is shown or dismissed.
/// </remarks>
public interface IDialogAware
{
    /// <summary>
    /// Determines whether the dialog window can be closed.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the dialog window can be closed; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// This method allows the ViewModel to control the closing behavior of the dialog, 
    /// such as preventing the dialog from being closed if certain conditions aren't met.
    /// </remarks>
    bool CanCloseDialog();

    /// <summary>
    /// Invoked when the associated dialog window is closed or dismissed.
    /// </summary>
    /// <remarks>
    /// Implement this method to execute logic such as cleanup or state preservation 
    /// when the dialog window is closed.
    /// </remarks>
    void OnDialogClosed();

    /// <summary>
    /// Invoked when the associated dialog window is opened or shown.
    /// </summary>
    /// <param name="parameters">
    /// A collection of key/value pairs passed to the dialog's ViewModel. This can be used 
    /// to initialize the dialog with specific data or state.
    /// </param>
    /// <remarks>
    /// Use this method to handle any setup or initialization logic that needs to be executed 
    /// when the dialog is first displayed.
    /// </remarks>
    void OnDialogOpened(DialogParameters parameters);
}
