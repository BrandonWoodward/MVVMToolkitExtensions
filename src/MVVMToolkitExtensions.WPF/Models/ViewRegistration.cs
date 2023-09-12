using System.Windows;
using MVVMToolkitExtensions.WPF.Interfaces;

namespace MVVMToolkitExtensions.WPF.Models;

public sealed class ViewRegistration<TView, TViewModel> : IViewRegistration
    where TView : FrameworkElement
    where TViewModel : class
{
    public Type ViewType => typeof(TView);
    public Type ViewModelType => typeof(TViewModel);
}
