namespace MVVMToolkitExtensions.Core.Interfaces;

internal interface IViewRegistry
{
    Type this[Type viewType] { get; set; }
    bool Contains(Type viewType);
    IEnumerable<Type> Views();
    IEnumerable<Type> ViewModels();
}