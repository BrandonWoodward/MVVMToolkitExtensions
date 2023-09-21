using MVVMToolkitExtensions.WPF.Controls;

namespace MVVMToolkitExtensions.WPF.Interfaces;

public interface INavigationRegistry
{
    NavigationContainer this[string regionName] { get; set; }
    bool Contains(string regionName);
}