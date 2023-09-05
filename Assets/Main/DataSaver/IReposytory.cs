public interface IReposytory
{
    void Save<T>(T data, string fileName);

    T Load<T>(string fileName);
}
