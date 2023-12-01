using System;
using System.IO;
using UnityEngine;

namespace Assets.Main.Scripts.SaveLoader
{
    public class LocalReposytory : IReposytory
    {
        public void Save<T>(T data, string fileName)
            where T : IData
        {
            var dataJson = JsonUtility.ToJson(data);

            string path = Application.persistentDataPath + $"/{fileName}.json";

            File.WriteAllText(path, dataJson);
        }

        public T Load<T>(string fileName)
            where T : IData

        {
            if (File.Exists(Application.persistentDataPath + $"/{fileName}.json"))
            {
                var dataJson = File.ReadAllText(Application.persistentDataPath + $"/{fileName}.json");

                var data = JsonUtility.FromJson<T>(dataJson);

                return data;
            }
            else
            {
                return Activator.CreateInstance<T>();
            }
        }
    }
}