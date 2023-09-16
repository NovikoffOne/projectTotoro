using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior;
using IJunior.TypedScenes;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private MapManager _mapManagerPrefab;

    private void Start()
    {
        if (MapManager.Instance != null)
            MapManager.Instance.gameObject.SetActive(false);
    }

    public void PlayLevel1()
    {
        if (MapManager.Instance != null)
            MapManager.Instance.gameObject.SetActive(true);

        SampleScene8x5.Load();
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Выход");
    }
}
