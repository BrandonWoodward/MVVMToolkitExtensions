namespace MVVMToolkitExtensions.Core.Interfaces;

internal interface IViewRegistration
{
    Type ViewType { get; }
    Type ViewModelType { get; }
}