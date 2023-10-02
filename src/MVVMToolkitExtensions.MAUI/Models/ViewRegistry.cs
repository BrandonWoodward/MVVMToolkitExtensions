using MVVMToolkitExtensions.MAUI.Interfaces;

namespace MVVMToolkitExtensions.MAUI.Models;

internal sealed class ViewRegistry : IViewRegistry
{
    private readonly Dictionary<Type, Type> _registry;

    public Type this[Type viewType]
    {
        get => _registry.TryGetValue(viewType, out var type)
            ? type : throw new KeyNotFoundException("View not found. Register a View/ViewModel using AddView<TView, TViewModel>()");
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

    public bool Contains(Type viewType)
        => _registry.ContainsKey(viewType);
}
