using LayerLab;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using IJunior.TypedScenes;
using System.Dynamic;

public class MapManager : MonoBehaviour, IDisposable,
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
    [SerializeField] private Camera _camera;

    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _interLevelMenu;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _pauseMenuPanel;
    [SerializeField] private GameObject _pauseButton;

    [SerializeField] private Player _player;

    [SerializeField] private int _minNumberPassengersCarried;
    [SerializeField] private int _numberPassengersCarried = 0;

    [SerializeField] private int GridIndex = 0;

    private PoolMono<Player> _playerPool;

    private GridGenerator _grid;

    private bool _canTransition => _numberPassengersCarried >= _minNumberPassengersCarried;

    private void Awake()
    {
        if (MapManager.Instance != null)
        {
            Destroy(this.gameObject);
            Dispose();
            return;
        }
        else
            Instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        SubscribeAll();
    }

    private void OnEnable()
    {
        Init();
        _grid.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        GridIndex = 0;
        _playerPool?.DeSpawnAll();
        _grid?.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        UnsubscribeAll();
    }

    public void NewLevel(int index)
    {
        _player = _playerPool.Spawn();

        _numberPassengersCarried = 0;

        GridIndex += index;

        if (_gridData.Count > GridIndex)
            _grid.NewGrid(_gridData[GridIndex]);
        else 
        {
            DespawnPlayer();
            IJunior.TypedScenes.MainMenu.Load(); // Заглушка
        }
    }

    public void DespawnPlayer()
    {
        _playerPool?.DeSpawn(_player);
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
        DespawnPlayer();

        OpenMenu(_gameOverPanel);
    }

    public void OnEvent(OnPlayerInsided var)
    {
        PlayerInsaeded();
    }

    public void OnEvent(OnButtonClickPlay var)
    {
        DespawnPlayer();

        NewLevel(1);
    }

    public void OnEvent(OnButtonClickReload var)
    {
        DespawnPlayer();

        NewLevel(0);
    }

    public void OnEvent(OnButtonClickPause var)
    {
        OpenMenu(_pauseMenuPanel);
    }

    private void Init()
    {
        if (_playerPool == null)
            _playerPool = new PoolPlayer<Player>(_player);

        if (_grid == null)
        {
            _grid = Instantiate(_gridPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            _grid.Init(_camera);
        }
        
        _canvas.SetActive(true);
        _camera.gameObject.SetActive(true);
    }

    private void SubscribeAll()
    {
        this.Subscribe<EnergyChangeEvent>();
        this.Subscribe<OnGameOver>();
        this.Subscribe<OnPlayerInsided>();
        this.Subscribe<OnButtonClickPlay>();
        this.Subscribe<OnButtonClickReload>();
        this.Subscribe<OnButtonClickPause>();
    }

    private void UnsubscribeAll()
    {
        this.Unsubscribe<EnergyChangeEvent>();
        this.Unsubscribe<OnGameOver>();
        this.Unsubscribe<OnPlayerInsided>();
        this.Unsubscribe<OnButtonClickPlay>();
        this.Unsubscribe<OnButtonClickReload>();
        this.Unsubscribe<OnButtonClickPause>();
    }

    private void PlayerInsaeded()
    {
        if (_canTransition)
        {
            DespawnPlayer();
            OpenMenu(_interLevelMenu);
            EventBus.Raise(new OnOpenMenu(false));
        }
        else
            throw new System.Exception("Нет питания");
    }

    private void OpenMenu(GameObject panel)
    {
        EventBus.Raise(new OnOpenMenu(false));

        _pauseButton.SetActive(false);

        Time.timeScale = 0;
        panel.SetActive(true);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}