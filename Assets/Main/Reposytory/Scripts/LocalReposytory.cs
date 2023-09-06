using Assets.Main.DataSaver;
using System.IO;
using UnityEngine;

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
        var dataJson = File.ReadAllText(Application.persistentDataPath + $"/{fileName}.json");

        var data = JsonUtility.FromJson<T>(dataJson);

        return data;
    }
}
