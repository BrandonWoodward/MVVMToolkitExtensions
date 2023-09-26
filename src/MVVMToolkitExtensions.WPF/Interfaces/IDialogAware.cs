using MVVMToolkitExtensions.Core.Interfaces;

namespace MVVMToolkitExtensions.WPF.Interfaces;

/// <summary>
/// Tap into the dialog lifecycle using these methods
/// </summary>
public interface IDialogAware
{
    /// <summary>
    /// Controls whether the window can be closed.
    /// </summary>
    bool CanCloseDialog();
    
    /// <summary>
    /// Called when the dialog is closed.
    /// </summary>
    void OnDialogClosed();
    
    /// <summary>
    /// Called when the dialog is opened.
    /// </summary>
    /// <param name="parameters">
    /// Key/value pairs to be passed to the dialog's ViewModel
    /// </param>
    void OnDialogOpened(IParameters parameters);
}
