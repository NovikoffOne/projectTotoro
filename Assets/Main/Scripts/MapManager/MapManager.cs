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
    IEventReceiver<ClickGameActionEvent>
{
    [SerializeField] private List<GridData> _gridData; 
    [SerializeField] private GridGenerator _gridPrefab; // Вынести в гриддату

    [SerializeField] private Player _player; // Вынести в гриддату

    [SerializeField] private int _minNumberPassengersCarried; // Вынести в гриддату
    [SerializeField] private int _numberPassengersCarried = 0; // Вынести в гриддату

    [SerializeField] private int GridIndex = 0; // 

    private PoolMono<Player> _playerPool;

    private GridGenerator _grid;

    private bool _canTransition => _numberPassengersCarried >= _minNumberPassengersCarried;

    private void Start()
    {
        Init();

        NewLevel(GridIndex);

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

    private void Init()
    {
        if (_playerPool == null)
            _playerPool = new PoolPlayer<Player>(_player);

        if (_grid == null)
        {
            _grid = Instantiate(_gridPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            _grid.Init(Camera.main);
        }
    }

    private void SubscribeAll()
    {
        this.Subscribe<EnergyChangeEvent>();
        this.Subscribe<ClickGameActionEvent>();
    }

    private void UnsubscribeAll()
    {
        this.Unsubscribe<EnergyChangeEvent>();
        this.Unsubscribe<ClickGameActionEvent>();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public void OnEvent(ClickGameActionEvent var)
    {
        switch (var.GameAction)
        {
            case GameAction.ClickPlay:

                break;

            case GameAction.ClickReload:
                DespawnPlayer();
                NewLevel(0);
                break;

            case GameAction.ClickNextLevel:
                DespawnPlayer();
                NewLevel(1);
                break;

            case GameAction.Completed:
                if (_canTransition)
                {
                    DespawnPlayer();
                    //OpenMenu(_interLevelMenu); не нужно, так как контроллер напрямую узнает о том что плеер дошел
                }
                else
                    Debug.Log("Нет питания");
                break;

            case GameAction.GameOver:
                DespawnPlayer();
                break;

            case GameAction.Exit:
                IJunior.TypedScenes.MainMenu.Load();
                break;

            case GameAction.Start:
                break;

            default:
                break;
        }
    }
}