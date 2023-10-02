using System.Collections;

namespace MVVMToolkitExtensions.MAUI.Models;

public class ParametersBase : IEnumerable
{
    private readonly Dictionary<string, object> _parameters;

    public ParametersBase()
    {
        _parameters = new();
    }

    protected ParametersBase(IDictionary<string, object> initialValues)
    {
        foreach(var item in initialValues)
        {
            _parameters[item.Key] = item.Value;
        }
    }

    public T Get<T>(string key)
        => _parameters.TryGetValue(key, out var value)
            ? (T)value
            : throw new KeyNotFoundException($"Parameter with key {key} not found.");

    public void Add(string key, object value)
        => _parameters[key] = value;

    public IEnumerator GetEnumerator()
        => _parameters.GetEnumerator();
}
