using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    [SerializeField] private Vector3 _startPosition = new Vector3(0, 0, -.3f);
    
    [SerializeField] private PlayerPrimitiv _playerPrefab;

    [SerializeField] private Charge _chargePrefab;

    public PlayerEnergyReserve EnergyTank { get; private set; }
    public ChargeChanger ChargeChanger { get; private set; }
    public PlayerMovement Movement { get; private set; }

    public event Action OnGameOver;

    private void Awake()
    {
        Instantiate(_playerPrefab, _startPosition, Quaternion.identity, transform);
    }

    private void Start()
    {
        this.ChargeChanger.InstantiateCharge();

        EnergyTank.OnTankValueChange += OnValueChanged;
    }

    private void OnEnable()
    {
        this.ChargeChanger = GetComponent<ChargeChanger>();

        Movement = GetComponent<PlayerMovement>();

        EnergyTank = GetComponent<PlayerEnergyReserve>();
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

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
