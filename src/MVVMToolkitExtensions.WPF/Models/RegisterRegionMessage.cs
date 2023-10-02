using CommunityToolkit.Mvvm.Messaging.Messages;
using MVVMToolkitExtensions.WPF.Controls;

namespace MVVMToolkitExtensions.WPF.Models;

internal class RegisterRegionMessage : ValueChangedMessage<(string Name, RegionControl Region)>
{
    public RegisterRegionMessage((string, RegionControl) value) : base(value) { }
}
