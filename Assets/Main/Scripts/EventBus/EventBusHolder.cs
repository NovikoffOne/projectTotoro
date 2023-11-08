using System;
using System.Collections.Generic;

public class EventBusHolder
{
    private Dictionary<Type, LinkedList<IBaceEventReceiver>> _receivers;

    public EventBusHolder()
    {
        _receivers = new Dictionary<Type, LinkedList<IBaceEventReceiver>>();
    }

    public void AddSubscribe<T>(IEventReceiver<T> receiver) where T : struct, IEvent
    {
        Type eventType = typeof(T);

        if (!_receivers.ContainsKey(eventType))
            _receivers[eventType] = new LinkedList<IBaceEventReceiver>();

        _receivers[eventType].AddLast(receiver);
    }

    public void RemoveUnsubscribe<T>(IEventReceiver<T> receiver) where T : struct, IEvent
    {
        Type eventType = typeof(T);

        if (!_receivers.ContainsKey(eventType))
            return;

        _receivers[eventType].Remove(receiver);
    }

    public void Raise<T>(T var) where T : struct, IEvent
    {
        Type eventType = typeof(T);

        if (!_receivers.ContainsKey(eventType))
            return;

        var node = _receivers[eventType].First;

        while (node != null)
        {
            ((IEventReceiver<T>)node.Value).OnEvent(var);

            node = node.Next;
        }
    }
}
