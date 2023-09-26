using MVVMToolkitExtensions.WPF.Controls;

namespace MVVMToolkitExtensions.WPF.Interfaces;

internal interface IRegionRegistry
{
    RegionControl this[string regionName] { get; set; }
    bool Contains(string regionName);
}