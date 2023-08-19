using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior;
using IJunior.TypedScenes;

public class MainMenu : MonoBehaviour
{
    public void PlayLevel1()
    {
        SampleScene.Load();
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Выход");
    }
}
