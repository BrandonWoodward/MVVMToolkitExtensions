namespace MVVMToolkitExtensions.WPF.Interfaces;

internal interface IViewRegistration
{
    Type ViewType { get; }
    Type ViewModelType { get; }
}