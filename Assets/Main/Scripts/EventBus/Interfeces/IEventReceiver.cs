public interface IEventReceiver<T> : IBaceEventReceiver
    where T : struct, IEvent
{
    void OnEvent(T var);
};
