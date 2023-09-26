namespace MVVMToolkitExtensions.MAUI.Interfaces;

public interface IViewFactory
{
    (TView View, object ViewModel) Create<TView>() 
        where TView : VisualElement;

    (VisualElement View, object ViewModel) Create(Type viewType);
}