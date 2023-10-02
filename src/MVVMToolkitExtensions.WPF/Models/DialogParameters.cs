namespace MVVMToolkitExtensions.WPF.Models;


/// <summary>
/// Represents a collection of parameters used in dialogs.
/// </summary>
public class DialogParameters : BaseParameters
{
    public DialogParameters() { }

    public DialogParameters(IDictionary<string, object> initialValues) : base(initialValues) { }

    internal static DialogParameters Empty => new();
}
