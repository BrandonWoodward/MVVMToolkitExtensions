using System;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;
using MVVMToolkitExtensions.WPF.Interfaces;

namespace AutoWireViewModel.Demos.WPF.ViewModels;

public partial class DialogAViewModel : ObservableObject, IDialogAware
{
    [ObservableProperty]
    private string _message = string.Empty;

    public bool CanCloseDialog() => true;

    public void OnDialogClosed() => Console.WriteLine("Dialog A closed.");

    public void OnDialogOpened(IParameters parameters)
    {
        var message = parameters.Get<string>("message");
        Console.WriteLine("Dialog B opened.");
        Message = message;
    }
}