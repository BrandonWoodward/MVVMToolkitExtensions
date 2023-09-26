namespace MVVMToolkitExtensions.MAUI.Interfaces;

internal interface INavigationRegistry {
    Type this[string route] { get; }
}