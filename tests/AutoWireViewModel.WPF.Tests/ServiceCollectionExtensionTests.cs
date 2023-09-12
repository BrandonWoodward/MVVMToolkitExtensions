using AutoWireViewModel.WPF.Tests.ViewModels;
using AutoWireViewModel.WPF.Tests.Views;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AutoWireViewModel.WPF.Tests;

public class ServiceCollectionExtensionTests
{
    [Fact]
    public void RegisterViewModels_ShouldRegisterCorrectTypes()
    {
        // Arrange
        var services = new ServiceCollection();
        var assembly = typeof(FakeView).Assembly;

        // Act
        services.RegisterViewModels(assembly);

        // Assert
        var registeredService = services.FirstOrDefault(s => s.ServiceType == typeof(FakeViewModel));
        registeredService.Should().NotBeNull();
        registeredService!.Lifetime.Should().Be(ServiceLifetime.Transient);
    }

    [Fact]
    public void RegisterViewModels_ShouldNotRegisterAbstractViewModels()
    {
        // Arrange
        var services = new ServiceCollection();
        var assembly = typeof(FakeView).Assembly;

        // Act
        services.RegisterViewModels(assembly);

        // Assert
        var registeredService = services.FirstOrDefault(s => s.ServiceType == typeof(FakeViewModelAbstract));
        registeredService.Should().BeNull();
    }

    [Fact]
    public void RegisterViewModels_ShouldNotRegisterNonViewModelTypes()
    {
        // Arrange
        var services = new ServiceCollection();
        var assembly = typeof(FakeView).Assembly;

        // Act
        services.RegisterViewModels(assembly);

        // Assert
        var registeredService = services.FirstOrDefault(s => s.ServiceType == typeof(FakeViewModelInvalidName));
        registeredService.Should().BeNull();
    }
}
