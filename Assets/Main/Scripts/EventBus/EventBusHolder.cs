using System;
using System.Collections.Generic;

public class EventBusHolder
{
    private Dictionary<Type, List<WeakReference<IBaceEventReceiver>>> _receivers; // Тип событий / список подписчиков
    private Dictionary<int, WeakReference<IBaceEventReceiver>> _receiverHashToReference; // hash события и подписчик

    public EventBusHolder()
    {
        _receivers = new Dictionary<Type, List<WeakReference<IBaceEventReceiver>>>();
        _receiverHashToReference = new Dictionary<int, WeakReference<IBaceEventReceiver>>();
    }

    public void AddSubscribe<T>(IEventReceiver<T> receiver) where T : struct, IEvenet
    {
        Type eventType = typeof(T);

        if (!_receivers.ContainsKey(eventType))
            _receivers[eventType] = new List<WeakReference<IBaceEventReceiver>>();

        WeakReference<IBaceEventReceiver> reference = new WeakReference<IBaceEventReceiver>(receiver);

        _receivers[eventType].Add(reference);

        _receiverHashToReference[receiver.GetHashCode()] = reference;
    }

    public void RemoveUnsubscribe<T>(IEventReceiver<T> receiver) where T : struct, IEvenet
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
