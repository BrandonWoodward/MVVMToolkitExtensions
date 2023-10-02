using MVVMToolkitExtensions.MAUI.Controls;
using MVVMToolkitExtensions.MAUI.Interfaces;
using MVVMToolkitExtensions.MAUI.Models;
using MVVMToolkitExtensions.MAUI.Services;
using NSubstitute;
using Xunit;

namespace MVVMToolkitExtensions.MAUI.Tests;

public class RegionManagerTests
{
    private readonly IViewFactory _viewFactory;
    private readonly IRegionRegistry _regionRegistry;
    private readonly IRegionManager _sut;

    private class StubView : View { }

    public RegionManagerTests()
    {
        _viewFactory = Substitute.For<IViewFactory>();
        _regionRegistry = Substitute.For<IRegionRegistry>();
        _sut = new RegionManager(_viewFactory, _regionRegistry);
    }

    [Fact]
    public void Navigate_ShouldThrowInvalidOperationException_WhenRegionDoesNotExist()
    {
        // Arrange
        const string regionName = "Region";
        _regionRegistry.Contains(regionName).Returns(false);

        // Act
        void Act() => _sut.Navigate<StubView>(regionName, new());

        // Assert
        Assert.Throws<InvalidOperationException>(Act);
    }

    [Fact]
    public void Navigate_ShouldClearAndSetNewContent_WhenRegionExists()
    {
        // Arrange
        const string regionName = "Region";
        _regionRegistry.Contains(regionName).Returns(true);
        var region = Substitute.For<RegionControl>();
        _regionRegistry[regionName].Returns(region);

        // Act
        _sut.Navigate<StubView>(regionName, new());

        // Assert
        _regionRegistry[regionName].Received(1).Content = null;
        _regionRegistry[regionName].Received(1).Content = Arg.Any<StubView>();
    }

    [Fact]
    public void Navigate_ShouldHandleNavigationAware_WhenBindingContextIsNavigationAware()
    {
        // Arrange
        const string regionName = "Region";
        var navAware = Substitute.For<INavigationAware>();
        var region = Substitute.For<RegionControl>();
        region.Content = new StubView { BindingContext = navAware };
        _regionRegistry.Contains(regionName).Returns(true);
        _regionRegistry[regionName].Returns(region);
        _viewFactory.Create<StubView>().Returns((new() { BindingContext = navAware }, navAware));

        // Act
        _sut.Navigate<StubView>(regionName, new());

        // Assert
        navAware.Received(1).OnNavigatedFrom();
        navAware.Received(1).OnNavigatedTo(Arg.Any<NavigationParameters>());
    }

    [Fact]
    public void Clear_ShouldClearRegionContent_WhenRegionExists()
    {
        // Arrange
        const string regionName = "Region";
        _regionRegistry.Contains(regionName).Returns(true);
        var region = Substitute.For<RegionControl>();
        _regionRegistry[regionName].Returns(region);

        // Act
        _sut.Clear(regionName);

        // Assert
        _regionRegistry[regionName].Received(1).Content = null;
    }


    [Fact]
    public void Clear_InvokesOnNavigatedFrom_WhenRegionContentHasNavigationAwareBindingContext()
    {
        // Arrange
        const string regionName = "Region";
        var navAware = Substitute.For<INavigationAware>();
        var region = Substitute.For<RegionControl>();
        region.Content = new StubView { BindingContext = navAware };
        _regionRegistry.Contains(regionName).Returns(true);
        _regionRegistry[regionName].Returns(region);

        // Act
        _sut.Clear(regionName);

        // Assert
        navAware.Received(1).OnNavigatedFrom();
    }
}