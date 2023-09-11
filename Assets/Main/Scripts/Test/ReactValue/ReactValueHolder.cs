using System;
using System.Collections.Generic;

public class ReactValueHolder<T, T2>
{
    //private Dictionary<IReactValue<T>, List<IObserverReactValue<T>>> _recever = new Dictionary<IReactValue<T>, List<IObserverReactValue<T>>>();
    private Dictionary<Type, List<IReactValue<T2>>> _recevers = new Dictionary<Type, List<IReactValue<T2>>>();

    public void Add(IObserveableReactValue<T> keyType, IReactValue<T2> reactValue)
    {
        Type key = typeof(T);

        if (!_recevers.ContainsKey(key))
            _recevers[key] = new List<IReactValue<T2>>();

        _recevers[key].Add(reactValue);
    }

    public void Remove(IObserveableReactValue<T> keyType, IReactValue<T2> recever)
    {
        Type key = typeof(T);

        if (_recevers.ContainsKey(key))
        {
            _recevers.Remove(key);
        }
    }

    //public T GetReactValue(IObserveableReactValue<T> keyType, IReactValue<T2> valueType)
    //{
        
    //}

    //public void Add(IReactValue<T> key, IObserverReactValue<T> recever)
    //{
    //    if (!_recever.ContainsKey(key))
    //        _recever[key] = new List<IObserverReactValue<T>>((IEnumerable<IObserverReactValue<T>>)recever);
    //    else
    //        _recever[key].Add(recever);
    //}


}
