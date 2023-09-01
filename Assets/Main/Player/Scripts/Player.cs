using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerView _playerView;

    [SerializeField] private Charge _chargePrefab;

    public PlayerEnergyReserve EnergyTank { get; private set; }
    public ChargeChanger ChargeChanger { get; private set; }
    public PlayerMovement Movement { get; private set; }
    public PlayerInput Input { get; private set; }

    public event Action OnGameOver;

    private void Awake()
    {
        Movement = new PlayerMovement(_playerView);

        EnergyTank = new PlayerEnergyReserve(50);

        Input = new PlayerInput(this);
    }

    private void Start()
    {
        this.ChargeChanger.InstantiateCharge();

        EnergyTank.OnTankValueChange += OnValueChanged;
    }

    private void Update()
    {
        Input.Update();
    }

    private void OnEnable()
    {
        this.ChargeChanger = GetComponent<ChargeChanger>();
    }

    private void OnDisable()
    {
        EnergyTank.OnTankValueChange -= OnValueChanged;
    }

    private void OnValueChanged(float newValue)
    {
        if(EnergyTank.HaveGas == false)
            OnGameOver?.Invoke();
    }
}
