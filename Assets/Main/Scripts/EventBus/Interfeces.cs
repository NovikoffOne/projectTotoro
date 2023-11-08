public interface IEvent { }; 

public interface IBaceEventReceiver { };

public interface IEventReceiver<T> : IBaceEventReceiver
    where T:struct,IEvent 
{
    void OnEvent(T var);
};
