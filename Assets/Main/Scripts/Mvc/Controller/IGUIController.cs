public interface IGUIController<V, M> : IController
    where V : class, IView
    where M : class, IModel, new()
{
    V View { get; }
    M Model { get; }

    void UpdateView();
}
