using System;
using System.Collections.Generic;

public class EventBusHolder
{
    private Dictionary<Type, LinkedList<IBaceEventReceiver>> _receivers; // Тип событий / список подписчиков
    //private Dictionary<int, WeakReference<IBaceEventReceiver>> _receiverHashToReference; // hash события и подписчик

    public EventBusHolder()
    {
        _receivers = new Dictionary<Type, LinkedList<IBaceEventReceiver>>();
      //  _receiverHashToReference = new Dictionary<int, WeakReference<IBaceEventReceiver>>();
    }

    public void AddSubscribe<T>(IEventReceiver<T> receiver) where T : struct, IEvenet
    {
        Type eventType = typeof(T);

        if (!_receivers.ContainsKey(eventType))
            _receivers[eventType] = new LinkedList<IBaceEventReceiver>();

       // WeakReference<IBaceEventReceiver> reference = new WeakReference<IBaceEventReceiver>(receiver);

        _receivers[eventType].AddLast(receiver);

        //_receiverHashToReference[receiver.GetHashCode()] = reference;
    }

    public void RemoveUnsubscribe<T>(IEventReceiver<T> receiver) where T : struct, IEvenet
    {
        Type eventType = typeof(T);

        int receiverHash = receiver.GetHashCode();

        if (!_receivers.ContainsKey(eventType))
            return;

        // WeakReference<IBaceEventReceiver> reference = _receiverHashToReference[receiverHash];


        _receivers[eventType].Remove(receiver);
        //_receiverHashToReference.Remove(receiverHash);
    }

    public void Raise<T>(T var) where T : struct, IEvenet
    {
        Type eventType = typeof(T);

        if (!_receivers.ContainsKey(eventType))
            return;

        var node = _receivers[eventType].First;

        while (node != null)
        {
            //if (node.Value.TryGetTarget(out IBaceEventReceiver receiver))
            //    ((IEventReceiver<T>)receiver).OnEvent(var);

            ((IEventReceiver<T>)node.Value).OnEvent(var);

            node = node.Next;
        }
    }
}
