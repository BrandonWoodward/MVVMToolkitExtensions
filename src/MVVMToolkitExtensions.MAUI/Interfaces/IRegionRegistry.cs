namespace MVVMToolkitExtensions.MAUI.Interfaces;
using MVVMToolkitExtensions.MAUI.Controls;

internal interface IRegionRegistry
{
    RegionControl this[string regionName] { get; set; }
    bool Contains(string regionName);
}