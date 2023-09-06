using Assets.Main.DataSaver;

public interface IReposytory
{
    void Save<T>(T data, string fileName) where T : IData;

    T Load<T>(string fileName) where T : IData;
}
