using System.Windows;

namespace MVVMToolkitExtensions.WPF.Interfaces;

internal interface IRegionControl
{
    string RegionName { get; set; }
    
    object DataContext { get; set; }
    
    void Add(FrameworkElement view);
    void Remove(FrameworkElement view);
    void Clear();
}