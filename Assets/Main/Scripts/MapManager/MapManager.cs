using LayerLab;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour,
    IEventReceiver<EnergyChangeEvent>,
    IEventReceiver<OnGameOver>,
    IEventReceiver<OnPlayerInsided>,
    IEventReceiver<OnButtonClickPlay>,
    IEventReceiver<OnButtonClickReload>,
    IEventReceiver<OnButtonClickPause>
{
    public static MapManager Instance { get; private set; }

    [SerializeField] private List<GridData> _gridData;
    [SerializeField] private GridGenerator _gridPrefab;

    [SerializeField] private GameObject _interLevelMenu;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _pauseMenuPanel;

    [SerializeField] private Player _player;

    [SerializeField] private int _minNumberPassengersCarried;
    [SerializeField] private int _numberPassengersCarried = 0;

    [SerializeField] private int GridIndex = 0;

    private GridGenerator _grid;

    private bool _canTransition => _numberPassengersCarried >= _minNumberPassengersCarried;

    private void Start()
    {
        if (Instance == null)
            Instance = this;

        DontDestroyOnLoad(this);

        _grid = Instantiate(_gridPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        _grid.Init(FindObjectOfType<Camera>());

        _grid.NewGrid(_gridData[GridIndex]);

        _player = Instantiate(_player, new Vector3(0, 0, -.3f), Quaternion.identity);

        _player.Init();

        SubscribeAll();
    }

    private void OnDisable()
    {
        UnsubscribeAll();
    }

    public void NewLevel(int index)
    {
        _numberPassengersCarried = 0;

        GridIndex += index;

        if (_gridData.Count > GridIndex)
            _grid.NewGrid(_gridData[GridIndex]);
        else
            IJunior.TypedScenes.MainMenu.Load(); // Заглушка
        
        _player.Reset();
    }

    public void OnEvent(EnergyChangeEvent var)
    {
        if (var.IsChargeChange)
            _numberPassengersCarried++;

        if (_canTransition)
            Debug.Log("Ворота открыты");
    }

    public void OnEvent(OnGameOver var)
    {
        OpenMenu(_gameOverPanel);
    }

    public void OnEvent(OnPlayerInsided var)
    {
        PlayerInsaeded();
    }

    public void OnEvent(OnButtonClickPlay var)
    {
        NewLevel(1);
    }

    public void OnEvent(OnButtonClickReload var)
    {
        NewLevel(0);
    }

    public void OnEvent(OnButtonClickPause var)
    {
        OpenMenu(_pauseMenuPanel);
    }

    private void SubscribeAll()
    {
        EventBus.Subscribe((IEventReceiver<EnergyChangeEvent>)this);
        EventBus.Subscribe((IEventReceiver<OnGameOver>)this);
        EventBus.Subscribe((IEventReceiver<OnPlayerInsided>)this);
        EventBus.Subscribe((IEventReceiver<OnButtonClickPlay>)this);
        EventBus.Subscribe((IEventReceiver<OnButtonClickReload>)this);
        EventBus.Subscribe((IEventReceiver<OnButtonClickPause>)this);
    }

    private void UnsubscribeAll()
    {
        EventBus.Unsubscribe((IEventReceiver<EnergyChangeEvent>)this);
        EventBus.Unsubscribe((IEventReceiver<OnGameOver>)this);
        EventBus.Unsubscribe((IEventReceiver<OnPlayerInsided>)this);
        EventBus.Unsubscribe((IEventReceiver<OnButtonClickPlay>)this);
        EventBus.Unsubscribe((IEventReceiver<OnButtonClickReload>)this);
        EventBus.Unsubscribe((IEventReceiver<OnButtonClickPause>)this);
    }

    private void PlayerInsaeded()
    {
        if (_canTransition)
        {
            OpenMenu(_interLevelMenu);
            EventBus.Raise(new OnOpenMenu(false));
        }
        else
            throw new System.Exception("Нет питания");
    }

    private void OpenMenu(GameObject panel)
    {
        EventBus.Raise(new OnOpenMenu(false));

        Time.timeScale = 0;
        panel.SetActive(true);
    }
}