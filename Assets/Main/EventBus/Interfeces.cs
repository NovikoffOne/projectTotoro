public interface IEvenet { }; //Маркер собитий

public interface IBaceEventReceiver { }; // Базовый интерфейс для слушателя

public interface IEventReceiver<T> : IBaceEventReceiver // Параметризированный интерфейс принимающий тип события
    where T:struct,IEvenet 
{
    void OnEvent(T var);
};
