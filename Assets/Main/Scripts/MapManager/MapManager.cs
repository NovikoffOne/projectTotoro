using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class MapManager :
    //IEventReceiver<EnergyChanged>,
    IEventReceiver<PlayerInsided>,
    IEventReceiver<GameActionEvent>,
    IEventReceiver<NewGamePlayed>,
    IEventReceiver<IsRewarded>
{
    private LevelGenerator _grid;
    private Player _player;
    private MapManagerData _mapManagerData;
    private PoolMono<Player> _playerPool;

    private StateMachine _stateMachine;

    #region StateMachine
    public int LevelIndex => _gridIndex;
    public LevelGenerator Grid => _grid;
    public Player Player => _player;
    public MapManagerData Data => _mapManagerData;
    public PoolMono<Player> PlayerPool => _playerPool;

    public StateMachine StateMachine => _stateMachine;

    #endregion

    private int _numberChargeCarried;
    private int _gridIndex;

    public MapManager(MapManagerData mapManagerData, PoolMono<Player> poolPlayer)
    {
        _stateMachine = new StateMachine();
        
        _grid = new LevelGenerator();

        _mapManagerData = mapManagerData;
        _playerPool = poolPlayer;

        _stateMachine.ChangeState<InstalizeState>(state => state.Target = this);
        //SubscribeAll();
    }

    ~MapManager()
    {
        UnsubscribeAll();
    }

    public bool IsCanTransition => _numberChargeCarried >= _mapManagerData.MinNumberPassengersCarried;

    public int GridIndex => _gridIndex;

    //public void NewLevel(int index = 0)
    //{
    //    if (_player == null || _player.gameObject.activeSelf == false)
    //        _player = _playerPool.Spawn();

    //    _numberPassengersCarried = 0;

    //    _gridIndex = index;

    //    if (_mapManagerData.GridData.Count > _gridIndex)
    //        _grid.NewLevel(_mapManagerData.GridData[_gridIndex]);
    //    else
    //    {
    //        DespawnPlayer();
    //        IJunior.TypedScenes.MainMenu.Load();
    //    }
    //}

    public void DespawnPlayer()
    {
        _player?.Movement.ResetPosition();
        _playerPool?.DeSpawn(_player);
    }

    public void OnEvent(IsRewarded isRewarded)
    {
        _stateMachine.ChangeState<LoopGameState>(state => state.Target = this);
    }

    //public void OnEvent(EnergyChanged isChargeChanged)
    //{
    //    if (!isChargeChanged.IsChargeChange && _gridIndex == 0)
    //        EventBus.Raise(new TutorialStateChanged(3));

    //    if (isChargeChanged.IsChargeChange)
    //        _numberPassengersCarried++;

    //    if (IsCanTransition)
    //    {
    //        EventBus.Raise(new OpenLevelTransition());

    //        if (_gridIndex == 0)
    //            EventBus.Raise(new TutorialStateChanged(5));
    //    }
    //}

    public void OnEvent(NewGamePlayed newLevelIndex)
    {
        ChangeNewLevelState(newLevelIndex.IndexLevel);
    }

    public void OnEvent(GameActionEvent gameAction)
    {
        switch (gameAction.GameAction)
        {
            case GameAction.Start:
                if (_stateMachine.CurrentState.Equals(typeof(LoopGameState)))
                {
                    _stateMachine.ChangeState<LoopGameState>(state=> state.Target = this);
                    ChangeTutorialState();
                }
                break;

            case GameAction.GameOver:
                _stateMachine.ChangeState<OpenPanelState>(state => state.Target = this);
                break;

            case GameAction.Pause:
                _stateMachine.ChangeState<OpenPanelState>(state => state.Target = this);
                break;

            case GameAction.ClickReload:
                ChangeNewLevelState(GridIndex);
                break;

            case GameAction.ClickNextLevel:
                ChangeNewLevelState(GridIndex + 1);
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
            StateMachine.ChangeState<OpenPanelState>();
        }
    }

    public void ChangeTutorialState()
    {
        if (_gridIndex == 0)
            EventBus.Raise(new TutorialStateChanged(0));
        else
            EventBus.Raise(new TutorialStateChanged(0, false));
    }

    #region StateMachine
    public void SubscribeAll()
    {
        //this.Subscribe<EnergyChanged>();
        this.Subscribe<GameActionEvent>();
        this.Subscribe<PlayerInsided>();
        this.Subscribe<NewGamePlayed>();
        this.Subscribe<IsRewarded>();
    }

    public void SetNumberCarried(int charge)
    {
        _numberChargeCarried = charge;
    }

    public void UnsubscribeAll()
    {
        //this.Unsubscribe<EnergyChanged>();
        this.Unsubscribe<GameActionEvent>();
        this.Unsubscribe<PlayerInsided>();
        this.Unsubscribe<NewGamePlayed>();
        this.Unsubscribe<IsRewarded>();
    }

    public void SpawnPlayer()
    {
        _player = _playerPool.Spawn();
    }

    public int NumberPassengersCarried
    {
        get => _numberChargeCarried;

        set
        {
            if (_numberChargeCarried == default)
                _numberChargeCarried = value;
        }
    }

    public void ChangeNewLevelState(int index = 0)
    {
        if(_player != null)
            DespawnPlayer();

        _stateMachine.ChangeState<NewLevelState>(state => state.Target = this);
    }
    #endregion
}