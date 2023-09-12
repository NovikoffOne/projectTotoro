using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;

public class ReactValueHolder<T>
{
    private static Dictionary<int, IReactValue<T>> _recevers = new Dictionary<int, IReactValue<T>>();

    public static void Add(string keyStr, IReactValue<T> reactValue)
    {
        var key = keyStr.GetHashCode();

        if (!_recevers.ContainsKey(key))
            _recevers[key] = reactValue;
    }

    public static void Remove(string keyStr)
    {
        var key = keyStr.GetHashCode();

        if (_recevers.ContainsKey(key))
        {
            _recevers[key].Dispose();
            _recevers.Remove(key);
        }
    }

    public static IReactValue<T> GetReactValue(string keyStr)
    {
        var key = keyStr.GetHashCode();

        if (_recevers.ContainsKey(key))
            return _recevers[key];

        return new ReactValue<T>(default);
    }
}
