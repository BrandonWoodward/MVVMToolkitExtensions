using MVVMToolkitExtensions.MAUI.Interfaces;

namespace MVVMToolkitExtensions.MAUI.Models;

internal sealed class ViewRegistration<TView, TViewModel, TBaseView> : IViewRegistration
    where TView : TBaseView
    where TViewModel : class
{
    public Type ViewType => typeof(TView);
    public Type ViewModelType => typeof(TViewModel);
}
