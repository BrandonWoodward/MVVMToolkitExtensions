using MVVMToolkitExtensions.MAUI.Interfaces;
using MVVMToolkitExtensions.MAUI.Models;

namespace MVVMToolkitExtensions.MAUI.Services;

internal sealed class NavigationService : INavigationService
{
    private readonly IPageFactory _pageFactory;
    private readonly INavigationRegistry _navigationRegistry;
    private readonly INavigationRoot _navigationRoot;

    public NavigationService(
        IPageFactory pageFactory,
        INavigationRegistry navigationRegistry,
        INavigationRoot navigationRoot)
    {
        _pageFactory = pageFactory;
        _navigationRegistry = navigationRegistry;
        _navigationRoot = navigationRoot;
    }

    public async Task NavigateAsync(string uri, NavigationParameters? parameters = null)
    {
        var cleanUri = uri.Trim();
        var segments = cleanUri.Split('/').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

        // Go to root
        if(cleanUri == "//")
        {
            await _navigationRoot.PopToRootAsync();
        }

        // Replace root
        else if(cleanUri.StartsWith("//"))
        {
            // Push the new root page onto the stack first.
            // Can't remove root page if it is currently being displayed.
            await PushPageAsync(segments[0], parameters);

            // Clear the entire navigation stack except for the page we just pushed.
            foreach(var page in _navigationRoot.NavigationStack.Reverse().Skip(1))
            {
                _navigationRoot.RemovePage(page);
            }

            // Handle the rest of the segments
            var absolutePathSegments = cleanUri[2..].Split('/').Where(s => !string.IsNullOrWhiteSpace(s));
            foreach(var segment in absolutePathSegments.Skip(1))
            {
                await PushPageAsync(segment, parameters);
            }
        }

        // Relative Path (e.g., "/ViewA/ViewB" or "../ViewB")
        else if(cleanUri.StartsWith("/") || uri.StartsWith("../"))
        {
            foreach(var segment in segments)
            {
                // If ".." pop but ensure root view remains
                if(segment == ".." && _navigationRoot.NavigationStack.Count > 1)
                {
                    await PopPageAsync();
                    continue;
                }

                // If it's a view name push onto the stack
                await PushPageAsync(segment, parameters);
            }
        }

        // Absolute Path (e.g., "ViewA/ViewB")
        else
        {
            // Clear all pages except the root.
            // NavigationStack is IReadOnlyList
            foreach(var page in _navigationRoot.NavigationStack.Reverse().Skip(1))
            {
                _navigationRoot.RemovePage(page);
            }

            foreach(var segment in segments)
            {
                await PushPageAsync(segment, parameters);
            }
        }
    }

    private async Task PushPageAsync(string uri, NavigationParameters? parameters)
    {
        // Create a new page and ViewModel.
        // This will throw if the page type is not registered.
        var pageType = _navigationRegistry[uri];
        var (page, viewModel) = _pageFactory.Create(pageType);

        // Handle OnNavigatedTo if ViewModel implements INavigationAware.
        if(viewModel is INavigationAware navigationAware)
            navigationAware.OnNavigatedTo(parameters ?? NavigationParameters.Empty);

        // Push page to navigation stack.
        await _navigationRoot.PushAsync(page);
    }

    private async Task PopPageAsync()
    {
        // Get current page's ViewModel before popping.
        var currentPage = _navigationRoot.NavigationStack[^1];
        var currentViewModel = currentPage?.BindingContext;

        // Handle OnNavigatedFrom if ViewModel implements INavigationAware.
        if(currentViewModel is INavigationAware navigationAware)
            navigationAware.OnNavigatedFrom();

        // Pop page from navigation stack.
        await _navigationRoot.PopAsync();
    }
}