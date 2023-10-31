using DG.Tweening.Plugins.Core.PathCore;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarFiller : MonoBehaviour
{
    [SerializeField] private GameObject _star1;
    [SerializeField] private GameObject _star2;
    [SerializeField] private GameObject _star3;

    [SerializeField] private LockImage _lock;
    [SerializeField] private GameObject _levelPanel;

    [SerializeField] private TMP_Text _text;

    public void FiilStars(int index)
    {
        if (index == 0 && PlayerPrefs.HasKey($"LevelStar {index}") == true)
        {
            DrawStars(PlayerPrefs.GetInt($"LevelStar {index}"));
            return;
        }
        else if(index == 0 && PlayerPrefs.HasKey($"LevelStar {index}") == false)
        {
            DrawStars(0);
            return;
        }

        var key = PlayerPrefs.GetInt($"LevelPassed {index-1}");

        switch (key)
        {
            case 1:
                if (PlayerPrefs.HasKey($"LevelStar {index}"))
                {
                    _levelPanel.SetActive(true);
                    _lock.HidePanel();
                    DrawStars(PlayerPrefs.GetInt($"LevelStar {index}"));
                }
                else
                {
                    _levelPanel.SetActive(true);
                    _lock.HidePanel();
                    DrawStars(0);
                }
                break;

            case 0:
                _levelPanel.SetActive(false);
                _text.gameObject.SetActive(false);
                break;

            default:
                _levelPanel.SetActive(false);
                _text.gameObject.SetActive(false);
                break;
        }
    }

    private void DrawStars(int countStars)
    {
        _lock.HidePanel();

        if (countStars > 0)
            _star1.SetActive(true);

        if (countStars > 1)
            _star2.SetActive(true);

        if (countStars > 2)
            _star3.SetActive(true);
    }
}
