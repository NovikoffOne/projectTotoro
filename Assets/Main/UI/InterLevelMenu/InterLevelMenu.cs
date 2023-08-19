using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior;
using IJunior.TypedScenes;
using UnityEngine.SceneManagement;

public class InterLevelMenu : MonoBehaviour
{
    public void OnButtonClickExit()
    {
        NewAction();
        IJunior.TypedScenes.MainMenu.Load();
    }

    public void OnButtonClickPlay()
    {
        NewAction();
        Level_1.Load();
    }

    public void OnButtonClickReload()
    {
        NewAction();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NewAction()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
