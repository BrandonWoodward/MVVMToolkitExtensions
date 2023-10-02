namespace MVVMToolkitExtensions.MAUI.Models;

/// <summary>
/// Represents a collection of key/value pairs used for passing parameters between ViewModels during navigation.
/// </summary>
/// <remarks>
/// <see cref="NavigationParameters"/> extends from <see cref="ParametersBase"/> to provide a standardized 
/// mechanism for passing data during navigation. This can be useful when needing to provide context or 
/// state to a view or ViewModel when navigating to it.
/// </remarks>
public class NavigationParameters : ParametersBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NavigationParameters"/> class.
    /// </summary>
    public NavigationParameters() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="NavigationParameters"/> class with the specified initial values.
    /// </summary>
    /// <param name="initialValues">The initial key/value pairs to include in the parameters.</param>
    public NavigationParameters(IDictionary<string, object> initialValues) : base(initialValues) { }

    internal static NavigationParameters Empty => new();
}
