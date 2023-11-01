using System.Windows;

namespace MVVMToolkitExtensions.WPF.Interfaces;

/// <summary>
/// Implement this interface to define custom region controls.
/// </summary>
public interface IRegionControl
{
    string RegionName { get; set; }

    IEnumerable<object> RegionContent();
    void Add(FrameworkElement view);
    void Remove(FrameworkElement view);
    void Clear();
}