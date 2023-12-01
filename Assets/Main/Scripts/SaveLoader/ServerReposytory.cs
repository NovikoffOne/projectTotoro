using System.IO;
using UnityEngine;

namespace Assets.Main.Scripts.SaveLoader
{
    public class ServerReposytory : IReposytory
    {
        public void Save<T>(T data, string fileName)
        where T : IData
        {
            var dataJson = JsonUtility.ToJson(data);

            string path = Application.streamingAssetsPath + $"/{fileName}.json";

            File.WriteAllText(path, dataJson);
        }

        public T Load<T>(string fileName)
        where T : IData
        {
            var dataJson = File.ReadAllText(Application.streamingAssetsPath + $"/{fileName}.json");

            var data = JsonUtility.FromJson<T>(dataJson);

            return data;
        }
    }
}