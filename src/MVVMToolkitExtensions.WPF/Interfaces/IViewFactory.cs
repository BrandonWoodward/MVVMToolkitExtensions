using System.Windows;

namespace MVVMToolkitExtensions.WPF.Interfaces;

internal interface IViewFactory
{
    (TView View, object ViewModel) Create<TView>() where TView : FrameworkElement;
    (FrameworkElement View, object ViewModel) Create(Type viewType);
}