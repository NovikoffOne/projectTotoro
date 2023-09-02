using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior;
using IJunior.TypedScenes;
using UnityEngine.SceneManagement;

public class InterLevelMenu : MonoBehaviour
{
    [SerializeField] private MapManager _mapManager;

    public void OnButtonClickExit()
    {
        NewAction();
        IJunior.TypedScenes.MainMenu.Load();
    }

    public void OnButtonClickPlay()
    {
        NewAction();
        _mapManager.NewLevel();
    }

    public void OnButtonClickReload()
    {
        NewAction();
        _mapManager.ReloadLevel();
    }

    public void NewAction()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
