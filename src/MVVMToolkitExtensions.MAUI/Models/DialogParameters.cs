namespace MVVMToolkitExtensions.MAUI.Models;


/// <summary>
/// Represents a collection of parameters used in dialogs.
/// </summary>
public class DialogParameters : ParametersBase
{
    public DialogParameters() { }

    public DialogParameters(IDictionary<string, object> initialValues) : base(initialValues) { }

    internal static DialogParameters Empty => new();
}
