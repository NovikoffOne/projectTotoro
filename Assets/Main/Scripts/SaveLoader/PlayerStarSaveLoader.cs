using UnityEngine;

public class PlayerStarSaveLoader
{
    [SerializeField] private string _fileName;

    public void LoadData()
    {
        //using (var data = RepositoryHelper.Load<PlayerStarData>(new ServerReposytory(), _fileName))
        // LevelStar.Instance.SetStar(data.Count);
    }

    public void SaveData()
    {
        var data = new PlayerStarData()
        {
            //Count = LevelStar.Instance.Count
        };
    }
}
