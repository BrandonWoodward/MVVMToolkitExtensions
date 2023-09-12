using MVVMToolkitExtensions.WPF.Models;

namespace MVVMToolkitExtensions.WPF.Interfaces;

internal interface IDialogRegistry
{
    DialogRegistration this[string dialogName] { get; set; }
    bool Contains(string dialogName);
    IEnumerator<DialogRegistration> GetEnumerator();
}