namespace MVVMToolkitExtensions.MAUI.Interfaces; 

internal interface INavigationRegistration 
{
    public string Route { get; }
    public Type PageType { get; }
}