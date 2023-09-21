namespace MVVMToolkitExtensions.WPF.Interfaces;

public interface IViewRegistration
{
    Type ViewType { get; }
    Type ViewModelType { get; }
}