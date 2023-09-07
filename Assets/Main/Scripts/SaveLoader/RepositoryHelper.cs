public static class RepositoryHelper
{
    public static void Save<T>(T data, IReposytory saver, string fileName)
        where T : class, IData, new()
    {
        saver.Save(data, fileName);
    }

    public static T Load<T>(IReposytory loader, string fileName)
        where T : class, IData, new()
    {
        var data = loader.Load<T>(fileName);

        return data ?? new T();
    }
}
