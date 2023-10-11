using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using IJunior.TypedScenes;
using Agava;
using Agava.YandexGames;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private MapManagerData _mapManagerData;
    [SerializeField] private Player _playerPrefab;

    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif

        // Always wait for it if invoking something immediately in the first scene.
        yield return YandexGamesSdk.Initialize();

        if (PlayerAccount.IsAuthorized == false)
            PlayerAccount.StartAuthorizationPolling(1500);

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
        new LevelStar(mapManager);
        new LiderBoard();
        //YandexGamesSdk.Initialize();    
    }
}
