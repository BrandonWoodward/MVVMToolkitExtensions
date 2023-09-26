namespace MVVMToolkitExtensions.MAUI.Interfaces; 

public interface IPageFactory
{
    (TPage Page, object ViewModel) Create<TPage>() 
        where TPage : Page;

    (Page View, object ViewModel) Create(Type viewType);
}