using MVVMToolkitExtensions.MAUI.Controls;

namespace MVVMToolkitExtensions.MAUI.Interfaces;

internal interface IRegionRegistry
{
    RegionControl this[string regionName] { get; set; }
    bool Contains(string regionName);
}