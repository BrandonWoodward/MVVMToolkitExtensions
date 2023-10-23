using CommunityToolkit.Mvvm.ComponentModel;

namespace WPFDemo.ViewModels
{
    internal partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _title = "MVVMToolkitExtensions Guide demo";

        [ObservableProperty]
        private string _hello = "Hello World";
    }
}