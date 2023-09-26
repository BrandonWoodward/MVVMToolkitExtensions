using System.Collections;
using MVVMToolkitExtensions.Core.Interfaces;

namespace MVVMToolkitExtensions.Core.Models;

public class BaseParameters : IParameters, IEnumerable
{
    private readonly Dictionary<string, object> _parameters = new();

    protected BaseParameters() { }

    protected BaseParameters(IDictionary<string, object> initialValues)
    {
        foreach(var item in initialValues)
        {
            _parameters[item.Key] = item.Value;
        }
    }


    public T Get<T>(string key) => _parameters.TryGetValue(key, out var value) ? (T)value : throw new KeyNotFoundException();
    public void Add(string key, object value) => _parameters[key] = value;

    public IEnumerator GetEnumerator() => _parameters.GetEnumerator();
}
