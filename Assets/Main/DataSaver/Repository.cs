namespace Assets.Main.DataSaver
{
    internal static class Repository
    {
        public static void Save<T>(T data, IReposytory saver, string fileName)
        {
            saver.Save(data, fileName);
        }

        public static T Load<T>(IReposytory loader, string fileName)
        {
            return loader.Load<T>(fileName);
        }
    }
}
