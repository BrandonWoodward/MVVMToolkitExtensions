using MVVMToolkitExtensions.Core.Models;

namespace MVVMToolkitExtensions.MAUI.Interfaces;

public interface INavigationService 
{
    Task NavigateAsync(string uri, NavigationParameters parameters = null);
}