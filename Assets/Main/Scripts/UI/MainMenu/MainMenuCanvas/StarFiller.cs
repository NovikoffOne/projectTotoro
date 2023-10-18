using DG.Tweening.Plugins.Core.PathCore;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using PlayerPrefs = Agava.YandexGames.PlayerPrefs;

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
            Debug.Log($"@@@{index} == 0 && PlayerPrefs.HasKey(LevelStar {index}) == true");
            DrawStars(PlayerPrefs.GetInt($"LevelStar {index}"));
            return;
        }
        else if(index == 0 && PlayerPrefs.HasKey($"LevelStar {index}") == false)
        {
            Debug.Log($"@@@{index} == 0 && PlayerPrefs.HasKey(LevelStar {index}) == false");
            DrawStars(0);
            return;
        }

        var key = PlayerPrefs.GetInt($"LevelPassed {index-1}");

        Debug.Log($"@@@Level Passed = {key} index = {index}");

        switch (key)
        {
            case 1:
                if (PlayerPrefs.HasKey($"LevelStar {index}"))
                {
                    Debug.Log($"@@@Case 1");
                    _levelPanel.SetActive(true);
                    _lock.HidePanel();
                    DrawStars(PlayerPrefs.GetInt($"LevelStar {index}"));
                }
                else
                {
                    Debug.Log($"@@@Else Case 1");
                    _levelPanel.SetActive(true);
                    _lock.HidePanel();
                    DrawStars(0);
                }
                break;

            case 0:
                Debug.Log($"@@@Case 0");
                _levelPanel.SetActive(false);
                _text.gameObject.SetActive(false);
                break;

            default:
                Debug.Log($"@@@Case Default");
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
