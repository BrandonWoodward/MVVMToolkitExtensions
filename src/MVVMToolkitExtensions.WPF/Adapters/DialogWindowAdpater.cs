using System.ComponentModel;
using System.Windows;
using MVVMToolkitExtensions.WPF.Interfaces;

namespace MVVMToolkitExtensions.WPF.Adapters;

internal class DialogWindowAdapter : IDialogWindow
{
    private readonly Window _window;

    internal DialogWindowAdapter(FrameworkElement control) 
    {
        // If control not window, wrap in a window
        _window = control as Window ?? new() { Content = control };
    }

    public object DataContext
    {
        get => _window.DataContext;
        set => _window.DataContext = value;
    }

    public event CancelEventHandler Closing
    {
        add => _window.Closing += value;
        remove => _window.Closing -= value;
    }

    public event EventHandler Closed
    {
        add => _window.Closed += value;
        remove => _window.Closed -= value;
    }

    public bool? ShowDialog() => _window.ShowDialog();
}
