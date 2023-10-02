using System.Windows;
using MVVMToolkitExtensions.WPF.Interfaces;
using MVVMToolkitExtensions.WPF.Models;

namespace MVVMToolkitExtensions.WPF.Services;

internal sealed class DialogService : IDialogService
{
    private readonly IDialogFactory _dialogWindowFactory;
    private IDialogWindow? _currentView;

    public DialogService(IDialogFactory dialogWindowFactory)
    {
        _dialogWindowFactory = dialogWindowFactory;
    }

    public void ShowDialog<TView>(DialogParameters? parameters = null) where TView : FrameworkElement
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

    private DialogService HandleDialogAware(DialogParameters? parameters)
    {
        if (_currentView?.DataContext is not IDialogAware dialogAwareViewModel) return this;
        _currentView.Closing += (_, e) => e.Cancel = !dialogAwareViewModel.CanCloseDialog();
        _currentView.Closed += (_, _) => dialogAwareViewModel.OnDialogClosed();
        dialogAwareViewModel.OnDialogOpened(parameters ?? DialogParameters.Empty);
        return this;
    }

    private void Show()
    {
        _currentView?.ShowDialog();
    }
}

