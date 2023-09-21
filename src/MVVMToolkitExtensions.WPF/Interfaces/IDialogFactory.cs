using System.Windows;

namespace MVVMToolkitExtensions.WPF.Interfaces;

public interface IDialogFactory
{
    (IDialogWindow View, object ViewModel) Create<TView>() where TView : FrameworkElement;
}
