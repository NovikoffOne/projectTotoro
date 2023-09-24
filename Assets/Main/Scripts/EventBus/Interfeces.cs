public interface IEvent { }; //Маркер собитий

public interface IBaceEventReceiver { }; // Базовый интерфейс для слушателя

public interface IEventReceiver<T> : IBaceEventReceiver // Параметризированный интерфейс принимающий тип события
    where T:struct,IEvent 
{
    void OnEvent(T var);
};
