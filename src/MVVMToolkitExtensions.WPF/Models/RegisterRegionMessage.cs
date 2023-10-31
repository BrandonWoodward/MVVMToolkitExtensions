using CommunityToolkit.Mvvm.Messaging.Messages;
using MVVMToolkitExtensions.WPF.Controls;
using MVVMToolkitExtensions.WPF.Interfaces;

namespace MVVMToolkitExtensions.WPF.Models;

internal sealed class RegisterRegionMessage : ValueChangedMessage<(string Name, IRegionControl Region)>
{
    public RegisterRegionMessage((string, IRegionControl) value) : base(value) { }
}
