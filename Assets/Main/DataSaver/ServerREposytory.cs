using System.IO;
using UnityEngine;

namespace Assets.Main.DataSaver
{
    internal class ServerREposytory : IReposytory
    {
        public void Save<T>(T data, string fileName)
        {
            var dataJson = JsonUtility.ToJson(data);

            string path = Application.streamingAssetsPath + $"/{fileName}.json";

            File.WriteAllText(path, dataJson);
        }

        public T Load<T>(string fileName)
        {
            var dataJson = File.ReadAllText(Application.streamingAssetsPath + $"/{fileName}.json");

            var data = JsonUtility.FromJson<T>(dataJson);

            return data;
        }
    }
}