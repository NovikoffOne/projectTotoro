using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;

public static class ReactValueHandler
{
    private static Dictionary<string, IDisposable> _recevers = new Dictionary<string, IDisposable>();

    public static IReactValue<T> Create<T>( T value, string key = "")
    {
        var reactValue = new ReactValue<T>(value);

        if (!string.IsNullOrEmpty(key))
            _recevers[key] = reactValue;
        
        return reactValue;
    }

    public static void Remove(string key)
    {
        if (_recevers.TryGetValue(key, out var reactValue))
        {
            reactValue.Dispose();
            _recevers.Remove(key);
        }
    }

    public static IReactValue<T> GetReactValue<T>(string key)
    {
        if (_recevers.TryGetValue(key, out var reactValue))
            return (IReactValue<T> )reactValue;

        return new ReactValue<T>(default);
    }
}
