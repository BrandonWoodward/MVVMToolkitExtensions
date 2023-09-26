using System.Windows;
using MVVMToolkitExtensions.Core.Interfaces;
using MVVMToolkitExtensions.WPF.Interfaces;

namespace MVVMToolkitExtensions.WPF.Services;

/// <summary>
/// Service used to show dialogs and pass parameters to them.
/// </summary>
internal sealed class DialogService : IDialogService
{
    private readonly IDialogFactory _dialogWindowFactory;
    private IDialogWindow? _currentView;

    public DialogService(IDialogFactory dialogWindowFactory)
    {
        _dialogWindowFactory = dialogWindowFactory;
    }

    public void ShowDialog<TView>(IParameters parameters) where TView : FrameworkElement
    {
        CreateDialog<TView>()
            .HandleDialogAware(parameters)
            .Show();
    }

    private DialogService CreateDialog<TView>() where TView : FrameworkElement
    {
        var (view, _) = _dialogWindowFactory.Create<TView>();
        _currentView = view;
        return this;
    }

    private DialogService HandleDialogAware(IParameters parameters)
    {
        if (_currentView?.DataContext is not IDialogAware dialogAwareViewModel) return this;
        _currentView.Closing += (_, e) => e.Cancel = !dialogAwareViewModel.CanCloseDialog();
        _currentView.Closed += (_, _) => dialogAwareViewModel.OnDialogClosed();
        dialogAwareViewModel.OnDialogOpened(parameters);
        return this;
    }

    private void Show()
    {
        _currentView?.ShowDialog();
    }
}

