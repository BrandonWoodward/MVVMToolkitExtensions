namespace MVVMToolkitExtensions.WPF.Interfaces;

public interface IParameters
{
    /// <summary>
    /// Attempt to get a value from the parameters
    /// Will throw an exception if the key is not found
    /// </summary>
    /// <param name="key">The key provided when creating the parameters</param>
    /// <typeparam name="T">The type of the value</typeparam>
    T Get<T>(string key);
    
    /// <summary>
    /// Add a key/value pair to the parameters
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    void Add(string key, object value);
}