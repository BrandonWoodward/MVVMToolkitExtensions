using MVVMToolkitExtensions.MAUI.Models;

namespace MVVMToolkitExtensions.MAUI.Interfaces;

public interface INavigationService 
{
    Task NavigateAsync(string uri, NavigationParameters parameters = null);
}