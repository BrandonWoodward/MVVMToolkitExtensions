namespace MVVMToolkitExtensions.Core.Interfaces;

internal interface IRegionRegistry<TRegionControl>
    where TRegionControl : class
{
    TRegionControl this[string regionName] { get; set; }
    bool Contains(string regionName);
}