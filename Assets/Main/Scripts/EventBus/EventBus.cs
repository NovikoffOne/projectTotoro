using System.Runtime.InteropServices;

public static class EventBus
{
    private static EventBusHolder _busHolder = new EventBusHolder();

    public static void Subscribe<T>(this IEventReceiver<T> receiver) where T : struct, IEvenet
    {
        _busHolder.AddSubscribe(receiver);
    }

    public static void Unsubscribe<T>(this IEventReceiver<T> receiver) where T : struct, IEvenet
    {
        _busHolder.RemoveUnsubscribe(receiver);
    }

    public static void Raise<T>(T var) where T : struct, IEvenet
    {
        _busHolder.Raise(var);
    }
}
