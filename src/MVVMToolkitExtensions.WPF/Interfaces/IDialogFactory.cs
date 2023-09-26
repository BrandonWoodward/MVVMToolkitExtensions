using System.Windows;

namespace MVVMToolkitExtensions.WPF.Interfaces;

internal interface IDialogFactory
{
    (IDialogWindow View, object ViewModel) Create<TView>() where TView : FrameworkElement;
}
