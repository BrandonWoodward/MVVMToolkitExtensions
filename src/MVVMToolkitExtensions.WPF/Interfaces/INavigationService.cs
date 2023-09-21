using System.Windows;
using MVVMToolkitExtensions.WPF.Models;

namespace MVVMToolkitExtensions.WPF.Interfaces;

public interface INavigationService
{
    void Navigate<TView>(string regionName, NavigationParameters parameters) where TView : FrameworkElement;
    void Clear(string regionName);
}