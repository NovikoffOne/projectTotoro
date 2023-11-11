public interface IController
{
    void AddView<T>(T view) where T : class, IView;
}
