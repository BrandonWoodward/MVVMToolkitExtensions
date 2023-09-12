using System;
using System.Windows.Input;

namespace AutoWireViewModel.Demos.WPF;

public class RelayCommand : ICommand
{
    private readonly Action _execute;

    public event EventHandler CanExecuteChanged;

    public RelayCommand(Action execute)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
    }

    public bool CanExecute(object parameter) => true;

    public void Execute(object parameter) => _execute();
}
