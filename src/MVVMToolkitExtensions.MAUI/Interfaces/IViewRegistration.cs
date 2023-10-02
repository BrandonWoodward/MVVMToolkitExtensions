namespace MVVMToolkitExtensions.MAUI.Interfaces;

internal interface IViewRegistration
{
    Type ViewType { get; }
    Type ViewModelType { get; }
}