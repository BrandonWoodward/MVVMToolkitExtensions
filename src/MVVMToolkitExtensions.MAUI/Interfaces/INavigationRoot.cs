namespace MVVMToolkitExtensions.MAUI.Interfaces;

internal interface INavigationRoot
{
    IReadOnlyList<Page> NavigationStack { get; }
    INavigation Navigation { get; }
    Task PushAsync(Page page);
    Task PopAsync();
    void RemovePage(Page page);
    Task PopToRootAsync();
}