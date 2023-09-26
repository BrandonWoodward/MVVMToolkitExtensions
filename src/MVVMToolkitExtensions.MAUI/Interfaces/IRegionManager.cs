using MVVMToolkitExtensions.Core.Models;

namespace MVVMToolkitExtensions.MAUI.Interfaces;

public interface IRegionManager
{
    void Navigate<TView>(string regionName, NavigationParameters parameters) where TView : View;
    void Clear(string regionName);
}