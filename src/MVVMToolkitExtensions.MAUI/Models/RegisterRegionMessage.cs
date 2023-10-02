using CommunityToolkit.Mvvm.Messaging.Messages;
using MVVMToolkitExtensions.MAUI.Controls;

namespace MVVMToolkitExtensions.MAUI.Models;

internal class RegisterRegionMessage : ValueChangedMessage<(string Name, RegionControl Region)>
{
    public RegisterRegionMessage((string, RegionControl) value) : base(value) { }
}
