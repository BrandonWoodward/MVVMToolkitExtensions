using AutoWireViewModel.Demos.WPF.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MVVMToolkitExtensions.Core.Interfaces;
using MVVMToolkitExtensions.Core.Models;
using MVVMToolkitExtensions.WPF.Interfaces;
using MVVMToolkitExtensions.WPF.Models;

namespace AutoWireViewModel.Demos.WPF.ViewModels;

public partial class ViewAViewModel : ObservableObject, INavigationAware
{
    private readonly IDialogService _dialogService;

    [ObservableProperty]
    private string _message = string.Empty;

    public ViewAViewModel(IDialogService dialogService)
    {
        _dialogService = dialogService;
    }

    [RelayCommand]
    private void ShowDialogA()
    {
        var parameters = new DialogParameters
        {
            { "message", "ViewA -> DialogA" },
        };
        _dialogService.ShowDialog<DialogA>(parameters);
    }

    [RelayCommand]
    private void ShowDialogB()
    {
        var parameters = new DialogParameters
        {
            { "message", "ViewA -> DialogB" },
        };
        _dialogService.ShowDialog<DialogB>(parameters);
    }

    public void OnNavigatedFrom() => throw new System.NotImplementedException();
    public void OnNavigatedTo(IParameters parameters) => Message = parameters.Get<string>("message");
}
