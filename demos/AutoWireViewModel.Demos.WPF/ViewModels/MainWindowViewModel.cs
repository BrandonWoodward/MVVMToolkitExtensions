using System.Windows.Input;

namespace AutoWireViewModel.Demos.WPF.ViewModels;

public class MainWindowViewModel : BaseViewModel
{
    private int _counter;
    public int Counter
    {
        get => _counter;
        set => SetProperty(ref _counter, value);
    }

    public ICommand IncrementCommand { get; }

    public MainWindowViewModel()
    {
        IncrementCommand = new RelayCommand(IncrementCounter);
    }

    private void IncrementCounter() => Counter++;
}
