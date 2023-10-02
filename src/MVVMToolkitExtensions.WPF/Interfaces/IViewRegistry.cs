namespace MVVMToolkitExtensions.WPF.Interfaces;

internal interface IViewRegistry
{
    Type this[Type viewType] { get; set; }
    bool Contains(Type viewType);
}