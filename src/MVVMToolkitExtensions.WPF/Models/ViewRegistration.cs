using MVVMToolkitExtensions.WPF.Interfaces;

namespace MVVMToolkitExtensions.WPF.Models;

internal sealed class ViewRegistration<TView, TViewModel, TBaseView> : IViewRegistration
    where TView : TBaseView
    where TViewModel : class
{
    public Type ViewType => typeof(TView);
    public Type ViewModelType => typeof(TViewModel);
}
