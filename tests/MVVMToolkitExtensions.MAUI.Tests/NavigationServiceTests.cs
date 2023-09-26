using MVVMToolkitExtensions.Core.Interfaces;
using MVVMToolkitExtensions.Core.Models;
using MVVMToolkitExtensions.MAUI.Interfaces;
using MVVMToolkitExtensions.MAUI.Services;
using NSubstitute;
using Xunit;

namespace MVVMToolkitExtensions.MAUI.Tests;

public class NavigationServiceTests
{
    private readonly IPageFactory _pageFactory;
    private readonly INavigationRegistry _navigationRegistry;
    private readonly INavigationRoot _navigationRoot;
    private readonly INavigationService _sut;
    
    public NavigationServiceTests()
    {
        _pageFactory = Substitute.For<IPageFactory>();
        _navigationRegistry = Substitute.For<INavigationRegistry>();
        _navigationRoot = Substitute.For<INavigationRoot>();
        _sut = new NavigationService(_pageFactory, _navigationRegistry, _navigationRoot);
    }
    
    [Fact]
    public async void NavigateAsync_ShouldNavigateToRoot_WhenUriIsDoubleSlash()
    {
        // Arrange
        const string uri = "//";
        
        // Act
        await _sut.NavigateAsync(uri, new());
        
        // Assert
        await _navigationRoot.Received(1).PopToRootAsync();
    }
    
    [Fact]
    public async void NavigateAsync_ShouldReplaceRoot_WhenUriStartsWithDoubleSlash()
    {
        // Arrange
        const string uri = "//ViewA/ViewB";
        var navAware = Substitute.For<INavigationAware>();
        _navigationRoot.NavigationStack.Returns(new[]
        {
            new Page {BindingContext = navAware},
            new Page {BindingContext = navAware}
        });
        
        // Act
        await _sut.NavigateAsync(uri, new());
        
        // Assert
        await _navigationRoot.Received(2).PushAsync(Arg.Any<Page>());
        _navigationRoot.Received(1).RemovePage(Arg.Any<Page>());
    }
    
    [Fact]
    public async void NavigateAsync_ShouldPushToStack_WhenUriStartsWithSlash()
    {
        // Arrange
        const string uri = "/ViewA/ViewB";
        
        // Act
        await _sut.NavigateAsync(uri, new());
        
        // Assert
        await _navigationRoot.Received(2).PushAsync(Arg.Any<Page>());
    }
    
    [Fact]
    public async void NavigateAsync_ShouldPopFromStack_WhenUriStartsWithDoubleDot()
    {
        // Arrange
        const string uri = "../ViewA/ViewB";
        var parameters = new NavigationParameters();
        _navigationRoot.NavigationStack.Returns(new[]
        {
            new Page {BindingContext = Substitute.For<INavigationAware>},
            new Page {BindingContext = Substitute.For<INavigationAware>}
        });
        
        // Act
        await _sut.NavigateAsync(uri, parameters);
        
        // Assert
        await _navigationRoot.Received(1).PopAsync();
        await _navigationRoot.Received(2).PushAsync(Arg.Any<Page>());
    }

    [Fact]
    public async void NavigateAsync_ShouldNotReplaceRoot_WhenUriIsAbsolute()
    {
        // Arrange
        const string uri = "ViewA/ViewB";
        _navigationRoot.NavigationStack.Returns(new[]
        {
            new Page {BindingContext = Substitute.For<INavigationAware>},
            new Page {BindingContext = Substitute.For<INavigationAware>}
        });
        
        // Act
        await _sut.NavigateAsync(uri, new());
        
        // Assert
        _navigationRoot.Received(1).RemovePage(Arg.Any<Page>());
        await _navigationRoot.Received(2).PushAsync(Arg.Any<Page>());
    }
}