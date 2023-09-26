using System.ComponentModel;

namespace MVVMToolkitExtensions.WPF.Interfaces;

internal interface IDialogWindow
{
    object DataContext { get; set; }

    event CancelEventHandler Closing;
    event EventHandler Closed;

    bool? ShowDialog();
}