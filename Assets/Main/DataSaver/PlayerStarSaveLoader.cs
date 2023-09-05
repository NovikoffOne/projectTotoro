using Assets.Main.DataSaver;
using UnityEngine;

public class PlayerStarSaveLoader
{
    [SerializeField] private string _fileName;

    private ServerREposytory _reposytory = new ServerREposytory();

    public void LoadData()
    {
        var data = Repository.Load<PlayerStarData>(_reposytory, _fileName);

        PlayerStar.Instance.SetStar(data.Count);
    }

    public void SaveData()
    {
        var data = new PlayerStarData(PlayerStar.Instance.Count);

        Repository.Save(data, _reposytory, _fileName);
    }
}
