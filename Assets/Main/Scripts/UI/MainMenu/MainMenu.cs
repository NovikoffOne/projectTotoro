using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior;
using IJunior.TypedScenes;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private MapManager _mapManagerPrefab;

    public void PlayLevel1()
    {
        SampleScene8x5.Load();
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Выход");
    }
}
