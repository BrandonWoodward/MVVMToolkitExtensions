namespace MVVMToolkitExtensions.MAUI.Models;

/// <summary>
/// A set of key/value pairs that can be used to pass parameters between ViewModels.
/// </summary>
public class NavigationParameters : ParametersBase
{
    public NavigationParameters() { }

    public NavigationParameters(IDictionary<string, object> initialValues) : base(initialValues) { }

    internal static NavigationParameters Empty => new();
}
