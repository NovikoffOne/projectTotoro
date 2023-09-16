using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IEventReceiver<OnTankValueChange>
{
    [SerializeField] private PlayerView _playerView;

    [SerializeField] private Charge _chargePrefab;

    [SerializeField] private int _energyReserve = 20;

    [SerializeField] private ChargeChanger _chargeChanger;

    public PlayerEnergyReserve EnergyTank { get; private set; }
    public ChargeChanger ChargeChanger { get => _chargeChanger; }
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
        EventBus.Subscribe(this);
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
        EventBus.Unsubscribe(this);
    }

    public void OnEvent(OnTankValueChange var)
    {
        if (!EnergyTank.HaveGas)
            EventBus.Raise(new OnGameOver());
    }
}
