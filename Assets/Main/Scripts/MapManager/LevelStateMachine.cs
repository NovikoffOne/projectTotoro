public class LevelStateMachine :
    IEventReceiver<PlayerInsided>,
    IEventReceiver<GameActionEvent>,
    IEventReceiver<NewGamePlayed>,
    IEventReceiver<GameStarted>
{
    private LevelGenerator _grid;
    private Player _player;
    private MapManagerData _mapManagerData;
    private PoolMono<Player> _playerPool;
    private StateMachine _stateMachine;

    private int _numberChargeCarried;
    private int _gridIndex;

    public LevelStateMachine(MapManagerData mapManagerData, PoolMono<Player> poolPlayer)
    {
        _stateMachine = new StateMachine();
        _grid = new LevelGenerator();

        _mapManagerData = mapManagerData;
        _playerPool = poolPlayer;

        _stateMachine.ChangeState<InstalizeState>(state => state.Target = this);

        SubscribeAll();
    }

    ~LevelStateMachine()
    {
        UnsubscribeAll();
    }

    public LevelGenerator Grid => _grid;
    public Player Player => _player;
    public MapManagerData Data => _mapManagerData;
    public StateMachine StateMachine => _stateMachine;

    public int GridIndex => _gridIndex;
    public bool IsCanTransition => _numberChargeCarried >= _mapManagerData.MinNumberPassengersCarried;

    public void DespawnPlayer()
    {
        _player?.Movement.ResetPosition();
        _playerPool?.DeSpawn(_player);
    }

    public void OnEvent(PlayerInsided playerInsided)
    {
        if (IsCanTransition)
        {
            DespawnPlayer();
            EventBus.Raise(new GameActionEvent(GameAction.Completed));
        }
    }

    public void StartTutorial()
    {
        if (_gridIndex == 0)
            EventBus.Raise(new TutorialStateChanged(0, true));
        else
            EventBus.Raise(new TutorialStateChanged(0, false));
    }

    public void SubscribeAll()
    {
        this.Subscribe<GameActionEvent>();
        this.Subscribe<PlayerInsided>();
        this.Subscribe<NewGamePlayed>();
        this.Subscribe<GameStarted>();
    }

    public void SetNumberCarried(int charge)
    {
        _numberChargeCarried = charge;
    }

    public void UnsubscribeAll()
    {
        this.Unsubscribe<GameActionEvent>();
        this.Unsubscribe<PlayerInsided>();
        this.Unsubscribe<NewGamePlayed>();
        this.Unsubscribe<GameStarted>();
    }

    public void SpawnPlayer()
    {
        _player = _playerPool.Spawn();
    }

    public void OnEvent(GameStarted var)
    {
        _stateMachine.ChangeState<LoopGameState>(state => state.Target = this);
    }

    public void OnEvent(NewGamePlayed newLevelIndex)
    {
        ChangeNewLevelState(newLevelIndex.IndexLevel);
    }

    public void OnEvent(GameActionEvent gameAction)
    {
        switch (gameAction.GameAction)
        {
            case GameAction.Start:
                StartTutorial();
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

    private void ChangeNewLevelState(int index = 0)
    {
        if (_player != null)
            DespawnPlayer();

        _gridIndex = index;

        _stateMachine.ChangeState<NewLevelState>(state => state.Target = this);
    }
}