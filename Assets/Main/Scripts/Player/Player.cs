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

    private BoxCollider _boxCollider;

    private void Awake()
    {
        Movement = new PlayerMovement(_playerView);

        EnergyTank = new PlayerEnergyReserve(_energyReserve);

        Input = new PlayerInput(this);
    }


    private void Update()
    {
        Input.Update();
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    public void Init()
    {
        _chargeChanger.InstantiateCharge();

        EnergyTank.SetValueReserve(_energyReserve);

        EventBus.Subscribe(this);
    }


    public void Reset()
    {
        EventBus.Unsubscribe(this);

        _boxCollider = GetComponent<BoxCollider>();

        _boxCollider.enabled = false;

        Init();

        Movement.ResetPosition();

        StartCoroutine(MoveingStart());
    }

    public void OnEvent(OnTankValueChange var)
    {
        if (!EnergyTank.HaveGas)
            EventBus.Raise(new OnGameOver());
    }

    private IEnumerator MoveingStart() // πετ
    {
        while (_playerView.transform.position.x != 0 && _playerView.transform.position.y != 0)
        {
            yield return new WaitForSeconds(0.1f);
        }

        _boxCollider.enabled = true;
    }
}
