namespace MVVMToolkitExtensions.MAUI.Interfaces;

internal interface INavigationRoot
{
    INavigation Navigation { get; }
    IReadOnlyList<Page> NavigationStack { get; }
    IReadOnlyList<Page> ModalStack { get; }
    Task PushAsync(Page page);
    Task PopAsync();
    void RemovePage(Page page);
    Task PopToRootAsync();
    Task PushModalAsync(Page page);
    Task PopModalAsync();
}