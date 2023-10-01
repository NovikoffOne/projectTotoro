using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarFiller : MonoBehaviour
{
    [SerializeField] GameObject star1;
    [SerializeField] GameObject star2;
    [SerializeField] GameObject star3;

    private int _levelIndex;
    private int _countSrars;

    public void SetLevelIndex(int index)
    {
        _levelIndex = index;

        using (var data = RepositoryHelper.Load<PlayerStarData>(new LocalReposytory(), $"Level {_levelIndex}"))
        {
            if (data != null)
                _countSrars = data.Count;
            else
                _countSrars = 0;
        }

        FillStar();
    }

    public void FillStar()
    {
        if (_countSrars > 0)
            star1.SetActive(true);

        if (_countSrars > 1)
            star2.SetActive(true);

        if (_countSrars > 2)
            star3.SetActive(true);
    }
}
