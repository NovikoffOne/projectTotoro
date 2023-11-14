public class LevelStateMachine :
    IEventReceiver<NewGamePlayed>
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

        this.Subscribe<NewGamePlayed>();
    }

    ~LevelStateMachine()
    {
        this.Unsubscribe<NewGamePlayed>();
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

    public void StartTutorial()
    {
        if (_gridIndex == 0)
            EventBus.Raise(new TutorialStateChanged(0, true));
        else
            EventBus.Raise(new TutorialStateChanged(0, false));
    }

    public void SetNumberCarried(int charge)
    {
        _numberChargeCarried = charge;
    }

    public void SpawnPlayer()
    {
        _player = _playerPool.Spawn();
    }

    public void PlayGame()
    {
        EventBus.Raise(new GameStarted(_gridIndex));
        
        _stateMachine.ChangeState<LoopGameState>(state => state.Target = this);
    }

    public void OnEvent(NewGamePlayed newLevelIndex)
    {
        ChangeNewLevelState(newLevelIndex.IndexLevel);
    }

    public void ChangeNewLevelState(int index = 0)
    {
        if (_player != null)
            DespawnPlayer();

        _gridIndex = index;

        _stateMachine.ChangeState<NewLevelState>(state => state.Target = this);
    }
}