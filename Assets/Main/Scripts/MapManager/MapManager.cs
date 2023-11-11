using UnityEngine;

public class MapManager :
    IEventReceiver<EnergyChanged>,
    IEventReceiver<PlayerInsided>,
    IEventReceiver<GameActionEvent>,
    IEventReceiver<NewGamePlayed>,
    IEventReceiver<IsRewarded>
{
    private LevelGenerator _grid;
    private Player _player;
    private MapManagerData _mapManagerData;
    private PoolMono<Player> _playerPool;

    private int _numberPassengersCarried;
    private int _gridIndex;

    public MapManager(MapManagerData mapManagerData, PoolMono<Player> poolPlayer)
    {
        _grid = new LevelGenerator();

        _mapManagerData = mapManagerData;
        _playerPool = poolPlayer;

        SubscribeAll();
    }

    ~MapManager()
    {
        UnsubscribeAll();
    }

    private bool IsCanTransition => _numberPassengersCarried >= _mapManagerData.MinNumberPassengersCarried;

    public int GridIndex => _gridIndex;

    public void NewLevel(int index = 0)
    {
        if (_player == null || _player.gameObject.activeSelf == false)
            _player = _playerPool.Spawn();

        _numberPassengersCarried = 0;

        _gridIndex = index;

        if (_mapManagerData.GridData.Count > _gridIndex)
            _grid.NewLevel(_mapManagerData.GridData[_gridIndex]);
        else
        {
            DespawnPlayer();
            IJunior.TypedScenes.MainMenu.Load();
        }
    }

    public void DespawnPlayer()
    {
        _player.Movement.ResetPosition();
        _playerPool?.DeSpawn(_player);
    }

    public void OnEvent(IsRewarded isRewarded)
    {
        EventBus.Raise(new PlayerCanInputed(true));
    }

    public void OnEvent(EnergyChanged isChargeChanged)
    {
        if (!isChargeChanged.IsChargeChange && _gridIndex == 0)
            EventBus.Raise(new TutorialStateChanged(3));

        if (isChargeChanged.IsChargeChange)
            _numberPassengersCarried++;

        if (IsCanTransition)
        {
            EventBus.Raise(new OpenLevelTransition());

            if (_gridIndex == 0)
                EventBus.Raise(new TutorialStateChanged(5));
        }
    }

    public void OnEvent(NewGamePlayed newLevelIndex)
    {
        NewLevel(newLevelIndex.IndexLevel);

        EventBus.Raise(new GameStarted(_gridIndex));
    }

    public void OnEvent(GameActionEvent gameAction)
    {
        switch (gameAction.GameAction)
        {
            case GameAction.ClickReload:
                DespawnPlayer();
                NewLevel(_gridIndex);
                break;

            case GameAction.ClickNextLevel:
                DespawnPlayer();
                NewLevel(++_gridIndex);
                break;

            case GameAction.GameOver:
                EventBus.Raise(new PlayerCanInputed(false));
                break;

            case GameAction.Start:
                ChangeTutorialState();
                break;

            case GameAction.Exit:
                DespawnPlayer();
                IJunior.TypedScenes.MainMenu.Load();
                break;

            default:
                break;
        }
    }

    public void OnEvent(PlayerInsided playerInsided)
    {
        if (IsCanTransition)
        {
            EventBus.Raise(new GameActionEvent(GameAction.Completed));
            DespawnPlayer();
        }
        else
            Debug.Log("Нет питания");
    }

    private void ChangeTutorialState()
    {
        if (_gridIndex == 0)
            EventBus.Raise(new TutorialStateChanged(0));
        else
            EventBus.Raise(new TutorialStateChanged(0, false));
    }

    private void SubscribeAll()
    {
        this.Subscribe<EnergyChanged>();
        this.Subscribe<GameActionEvent>();
        this.Subscribe<PlayerInsided>();
        this.Subscribe<NewGamePlayed>();
        this.Subscribe<IsRewarded>();
    }

    private void UnsubscribeAll()
    {
        this.Unsubscribe<EnergyChanged>();
        this.Unsubscribe<GameActionEvent>();
        this.Unsubscribe<PlayerInsided>();
        this.Unsubscribe<NewGamePlayed>();
        this.Unsubscribe<IsRewarded>();
    }
}