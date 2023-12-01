using Assets.Main.Scripts.Fsm;
using Assets.Main.Scripts.Generator;
using Assets.Main.Scripts.PlayerEnity;
using Assets.Main.Scripts.Pool;

namespace Assets.Main.Scripts.LevelFSM
{
    public class LevelStateMachine
    {
        private LevelGenerator _grid;
        private Player _player;
        private MapManagerData _mapManagerData;
        private PoolMono<Player> _playerPool;
        private StateMachine _stateMachine;

        private int _gridIndex;

        public LevelStateMachine(MapManagerData mapManagerData, PoolMono<Player> poolPlayer)
        {
            _stateMachine = new StateMachine();
            _grid = new LevelGenerator();

            _mapManagerData = mapManagerData;
            _playerPool = poolPlayer;

            _stateMachine.ChangeState<InstalizeState>(state => state.Target = this);
        }

        public Generator.LevelGenerator Grid => _grid;
        public PoolMono<Player> PlayerPool => _playerPool;
        public MapManagerData Data => _mapManagerData;
        public StateMachine StateMachine => _stateMachine;

        public int GridIndex
        {
            get
            {
                return _gridIndex;
            }
            set
            {
                if (value >= 0)
                    _gridIndex = value;
            }
        }

        public Player Player
        {
            get
            {
                return _player;
            }
            set
            {
                if (_player == null || _player.gameObject.activeSelf == false)
                {
                    _player = value;
                }
            }
        }
    }
}