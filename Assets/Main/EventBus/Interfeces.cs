public interface IEvenet { };

public interface IBaceEventReceiver { };

public interface IEventReceiver<T> : IBaceEventReceiver 
    where T:struct,IEvenet 
{
    void OnEvent(T var);
};
