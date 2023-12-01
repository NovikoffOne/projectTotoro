namespace Assets.Main.Scripts.SaveLoader
{
    public interface IReposytory
    {
        void Save<T>(T data, string fileName) where T : IData;

        T Load<T>(string fileName) where T : IData;
    }
}