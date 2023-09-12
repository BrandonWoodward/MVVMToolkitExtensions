using System.Collections;
using MVVMToolkitExtensions.WPF.Interfaces;

namespace MVVMToolkitExtensions.WPF.Models;

internal sealed class DialogRegistry : IDialogRegistry, IEnumerable<DialogRegistration>
{
    private readonly Dictionary<string, DialogRegistration> _registry = new();

    public DialogRegistration this[string dialogName]
    {
        get => _registry[dialogName];
        set => _registry[dialogName] = value;
    }

    public bool Contains(string dialogName)
        => _registry.ContainsKey(dialogName);

    public IEnumerator<DialogRegistration> GetEnumerator()
        => _registry.Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}
