namespace MVVMToolkitExtensions.WPF.Models;

public class NavigationParameters : BaseParameters
{
    public NavigationParameters() { }

    public NavigationParameters(IDictionary<string, object> initialValues) : base(initialValues) { }

    public static NavigationParameters Empty => new();
}
