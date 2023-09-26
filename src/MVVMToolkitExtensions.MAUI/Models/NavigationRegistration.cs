using MVVMToolkitExtensions.MAUI.Interfaces;

namespace MVVMToolkitExtensions.MAUI.Models; 

internal sealed class NavigationRegistration<TPage> : INavigationRegistration
    where TPage : Page
{
    public string Route => typeof(TPage).Name;
    public Type PageType => typeof(TPage);
}