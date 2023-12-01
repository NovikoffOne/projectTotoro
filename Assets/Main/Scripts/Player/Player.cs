using Assets.Main.Scripts.Events.GameEvents;
using UnityEngine;

namespace Assets.Main.Scripts.PlayerEnity
{
    public class Player : MonoBehaviour, IEventReceiver<OnTankValueChange>
    {
        [SerializeField] private PlayerView _playerView;

        [SerializeField] private Charge _chargePrefab;

        [SerializeField] private PlayerCharge _chargeChanger;

        [SerializeField] private int _energyReserve = 20;

        public PlayerEnergyReserve EnergyTank { get; private set; }
        public PlayerCharge ChargeChanger { get => _chargeChanger; }
        public PlayerMovement Movement { get; private set; }
        public PlayerInput Input { get; private set; }

        private void Awake()
        {
            Movement = new PlayerMovement(_playerView);

            EnergyTank = new PlayerEnergyReserve(_energyReserve);

            Input = new PlayerInput(this);
        }

        private void Start()
        {
            this.Subscribe();
        }

        private void Update()
        {
            Input.Update();
        }

        private void OnEnable()
        {
            _chargeChanger.InstantiateCharge();

            EnergyTank.SetValueReserve(_energyReserve);

            transform.position = new Vector3(0, 0, 0);

            Movement.ResetPosition();
        }

        private void OnDisable()
        {
            transform.position = new Vector3(0, 0, 0);

            Movement.ResetPosition();
        }

        private void OnDestroy()
        {
            this.Unsubscribe();
        }

        public void OnEvent(OnTankValueChange valueChanged)
        {
            if (!EnergyTank.HaveGas)
                EventBus.Raise(new GameActionEvent(GameAction.GameOver));
        }
    }
}