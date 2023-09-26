using MVVMToolkitExtensions.MAUI.Interfaces;

namespace MVVMToolkitExtensions.MAUI.Models;

internal sealed class NavigationRoot : INavigationRoot
{
    public INavigation Navigation => Application.Current?.MainPage?.Navigation 
                                     ?? throw new NullReferenceException();
    
    public IReadOnlyList<Page> ModalStack => Navigation.ModalStack; 
    
    public IReadOnlyList<Page> NavigationStack => Navigation.NavigationStack;
    
    public void RemovePage(Page page) => Navigation.RemovePage(page);
    
    public Task PushAsync(Page page) => Navigation.PushAsync(page); 
    
    public Task PopAsync() => Navigation.PopAsync();
    
    public Task PopToRootAsync() => Navigation.PopToRootAsync();
    
    public Task PushModalAsync(Page page) => Navigation.PushModalAsync(page);
    
    public Task PopModalAsync() => Navigation.PopModalAsync();
}