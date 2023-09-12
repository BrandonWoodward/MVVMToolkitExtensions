using AutoWireViewModel.WPF.Tests.InvalidNamespace;
using AutoWireViewModel.WPF.Tests.ViewModels;
using AutoWireViewModel.WPF.Tests.Views;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace AutoWireViewModel.WPF.Tests;

public class ViewModelLocatorTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ViewModelLocator _sut;

    public ViewModelLocatorTests()
    {
        _serviceProvider = Substitute.For<IServiceProvider>();
        _sut = new ViewModelLocator(_serviceProvider);
    }

    [WpfFact]
    public void LocateViewModelForView_ShouldReturnViewModel_WhenValidView()
    {
        // Arrange
        var view = new FakeView();
        var expectedViewModel = new FakeViewModel();
        _serviceProvider.GetService(typeof(FakeViewModel)).Returns(expectedViewModel);

        // Act
        var result = _sut.LocateViewModelForView(view);

        // Assert
        result.Should().Be(expectedViewModel);
    }

    [WpfFact]
    public void LocateViewModelForView_ShouldThrowInvalidOperationException_WhenNoViewModelFound()
    {
        // Arrange
        var view = new FakeViewNoViewModel();

        // Act
        var act = () => _sut.LocateViewModelForView(view);

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [WpfFact]
    public void LocateViewModelForView_ShouldThrowInvalidOperationException_WhenViewNamespaceDoesNotEndWithViews()
    {
        // Arrange
        var view = new FakeViewInvalidNamespace();

        // Act
        var act = () => _sut.LocateViewModelForView(view);

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }
}