namespace MVVMToolkitExtensions.Core.Models;


/// <summary>
/// Represents a collection of parameters used in dialogs.
/// </summary>
public class DialogParameters : BaseParameters
{
    public DialogParameters() { }

    public DialogParameters(IDictionary<string, object> initialValues) : base(initialValues) { }

    public static DialogParameters Empty => new();
}
