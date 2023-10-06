using DG.Tweening.Plugins.Core.PathCore;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarFiller : MonoBehaviour
{
    [SerializeField] GameObject star1;
    [SerializeField] GameObject star2;
    [SerializeField] GameObject star3;

    public void FiilStars(int index)
    {
        if (PlayerPrefs.HasKey($"Level {index}"))
            DrawStars(PlayerPrefs.GetInt($"Level {index}"));
        else
            DrawStars(0);
    }

    private void DrawStars(int countStars)
    {
        if (countStars > 0)
            star1.SetActive(true);

        if (countStars > 1)
            star2.SetActive(true);

        if (countStars > 2)
            star3.SetActive(true);
    }
}
