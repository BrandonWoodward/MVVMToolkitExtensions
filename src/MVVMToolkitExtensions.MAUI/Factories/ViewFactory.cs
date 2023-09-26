using MVVMToolkitExtensions.Core.Factories;
using MVVMToolkitExtensions.MAUI.Interfaces;

namespace MVVMToolkitExtensions.MAUI.Factories;

internal sealed class ViewFactory : FactoryBase<VisualElement>, IViewFactory
{
    public ViewFactory(IServiceProvider serviceProvider)
        : base(serviceProvider) { }

    public (TView View, object ViewModel) Create<TView>() where TView : VisualElement
        => CreateCore<TView>();

    public (VisualElement View, object ViewModel) Create(Type viewType)
        => CreateCore(viewType);
}
