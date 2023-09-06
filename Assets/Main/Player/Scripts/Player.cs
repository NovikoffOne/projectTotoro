using Assets.Main.EventBus.Events;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerView _playerView;

    [SerializeField] private Charge _chargePrefab;

    [SerializeField] private int _energyReserve = 20;

    public PlayerEnergyReserve EnergyTank { get; private set; }
    public ChargeChanger ChargeChanger { get; private set; }
    public PlayerMovement Movement { get; private set; }
    public PlayerInput Input { get; private set; }

    private void Awake()
    {
        Movement = new PlayerMovement(_playerView);

        EnergyTank = new PlayerEnergyReserve(_energyReserve);

        Input = new PlayerInput(this);

        this.ChargeChanger = GetComponent<ChargeChanger>();
    }

    public void Reset()
    {
        gameObject.SetActive(false);

        transform.position = new Vector3(0, 0, transform.position.z);

        gameObject.SetActive(true);

        this.ChargeChanger = GetComponent<ChargeChanger>();

        EnergyTank.SetValueReserve(_energyReserve);

        this.ChargeChanger.InstantiateCharge();

        EnergyTank.OnTankValueChange += OnValueChanged;
    }

    private void Update()
    {
        Input.Update();
    }

    private void OnDisable()
    {
        EnergyTank.OnTankValueChange -= OnValueChanged;
    }

    private void OnValueChanged(float newValue)
    {
        if (!EnergyTank.HaveGas)
            EventBus.Raise(new OnGameOver());
    }
}
