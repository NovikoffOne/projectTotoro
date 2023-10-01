using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using IJunior.TypedScenes;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private MapManagerData _mapManagerData;
    [SerializeField] private Player _playerPrefab;

    private void Start()
    {
        SceneManager.LoadScene(nameof(MainMenu));
        SceneManager.sceneLoaded += StartNewGame;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]

    private static void Initialize()
    {
        SceneManager.LoadScene(nameof(BootStrap));
    }

    private void StartNewGame(Scene scene, LoadSceneMode loadScene)
    {
        var mapManager = new MapManager(_mapManagerData, new PoolMono<Player>(_playerPrefab));
        mapManager.NewLevel();
        EventBus.Raise(new PlayerCanInput(false));
        SceneManager.sceneLoaded -= StartNewGame;
    }
}
