using Agava.YandexGames;
using IJunior.TypedScenes;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private MapManagerData _mapManagerData;
    [SerializeField] private Player _playerPrefab;

    private const string EnglishLanguage = "English";
    private const string RussianLanguage = "Russian";
    private const string TurkishLanguage = "Turkish";

    private const string AbbreviationEnglishLanguage = "en";
    private const string AbbreviationRussianLanguage = "ru";
    private const string AbbreviationTurkishLanguage = "tr";

    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        SceneManager.LoadScene(nameof(MainMenu));
        SceneManager.sceneLoaded += StartNewGame;

        yield break;
#endif
        yield return YandexGamesSdk.Initialize();

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

        new LiderBoard();
        new Ads();
        new LevelStar(mapManager);

#if !UNITY_WEBGL || UNITY_EDITOR
        return;
#endif
        DetectedLanguage(YandexGamesSdk.Environment.i18n.lang);
    }

    private void DetectedLanguage(string key)
    {
        switch (key)
        {
            case AbbreviationEnglishLanguage:
                Lean.Localization.LeanLocalization.SetCurrentLanguageAll(EnglishLanguage);
                break;

            case AbbreviationRussianLanguage:
                Lean.Localization.LeanLocalization.SetCurrentLanguageAll(RussianLanguage);
                break;

            case AbbreviationTurkishLanguage:
                Lean.Localization.LeanLocalization.SetCurrentLanguageAll(TurkishLanguage);
                break;

            default:
                Lean.Localization.LeanLocalization.SetCurrentLanguageAll(RussianLanguage);
                break;
        }
    }
}
