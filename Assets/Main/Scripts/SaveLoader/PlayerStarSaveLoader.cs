using Assets.Main.DataSaver;
using UnityEngine;

public class PlayerStarSaveLoader
{
    [SerializeField] private string _fileName;

    public void LoadData()
    {
        using (var data = RepositoryHelper.Load<PlayerStarData>(new ServerReposytory(), _fileName))
            PlayerStar.Instance.SetStar(data.Count);
    }

    public void SaveData()
    {
        var data = new PlayerStarData()
        {
            Count = PlayerStar.Instance.Count
        };

        RepositoryHelper.Save(data, new ServerReposytory(), _fileName);
    }
}
