using MVVMToolkitExtensions.WPF.Interfaces;

namespace MVVMToolkitExtensions.WPF.Models;

public sealed class ViewRegistry : IViewRegistry
{
    private readonly Dictionary<Type, Type> _registry;

    public Type this[Type viewType]
    {
        get => _registry[viewType];
        set => _registry[viewType] = value;
    }

    // At runtime, when IViewRegistry is resolved from the DI container,
    // all the ViewRegistration instances are passed to this
    public ViewRegistry(IEnumerable<IViewRegistration> viewRegistrations)
    {
        _registry = viewRegistrations.ToDictionary(
            viewRegistration => viewRegistration.ViewType,
            viewRegistration => viewRegistration.ViewModelType
        );
    }
    
    public IEnumerable<Type> Views() 
        =>  _registry.Keys.AsEnumerable();
    
    public IEnumerable<Type> ViewModels()
        => _registry.Values.AsEnumerable();

    public bool Contains(Type viewType)
        => _registry.ContainsKey(viewType);
}
