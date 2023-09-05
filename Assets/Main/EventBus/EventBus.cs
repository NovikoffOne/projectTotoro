using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBus 
{
    private Dictionary<Type, List<WeakReference<IBaceEventReceiver>>> _receivers;
    private Dictionary<int, WeakReference<IBaceEventReceiver>> _receiverHashToReference;

    public EventBus()
    {
        _receivers = new Dictionary<Type, List<WeakReference<IBaceEventReceiver>>>();
        _receiverHashToReference = new Dictionary<int, WeakReference<IBaceEventReceiver>>();
    }

    public void Subscribe<T>(IEventReceiver<T> receiver) where T:struct, IEvenet
    {
        Type eventType = typeof(T);

        if (!_receivers.ContainsKey(eventType))
            _receivers[eventType] = new List<WeakReference<IBaceEventReceiver>>();

        WeakReference<IBaceEventReceiver> reference = new WeakReference<IBaceEventReceiver>(receiver);

        _receivers[eventType].Add(reference);
        _receiverHashToReference[receiver.GetHashCode()] = reference;

    }

    public void Unsubscribe<T>(IEventReceiver<T> receiver) where T : struct, IEvenet
    {
        Type eventType = typeof(T);

        int receiverHash = receiver.GetHashCode();

        if (!_receivers.ContainsKey(eventType) || _receiverHashToReference.ContainsKey(receiverHash))
            return;

        WeakReference<IBaceEventReceiver> reference = _receiverHashToReference[receiverHash];

        _receivers[eventType].Remove(reference);
        _receiverHashToReference.Remove(receiverHash);
    }

    public void Raise<T>(T var) where T : struct, IEvenet
    {
        Type eventType = typeof(T);

        if (!_receivers.ContainsKey(eventType))
            return;

        foreach (WeakReference<IBaceEventReceiver> reference in _receivers[eventType])
        {
            if (reference.TryGetTarget(out IBaceEventReceiver receiver))
                ((IEventReceiver<T>)receiver).OnEvent(var);
        }
    }
}
