using MVVMToolkitExtensions.WPF.Controls;

namespace MVVMToolkitExtensions.WPF.Interfaces;

internal interface IRegionRegistry
{
    IRegionControl this[string regionName] { get; }
    bool Contains(string regionName);
}