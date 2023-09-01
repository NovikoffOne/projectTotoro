using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWigetLifeBar : MonoBehaviour
{
    [SerializeField] private EnergyBar _energyBar;
    [SerializeField] private Player _player;

    private EnergyReserve _tank;
    
    private void Start()
    {
        _tank = _player.EnergyTank;
        _tank.OnTankValueChange += OnTankValueChanged;
    }

    private void OnDisable()
    {
        _tank.OnTankValueChange -= OnTankValueChanged;
    }

    private void OnTankValueChanged(float newValueNormalized)
    {
        _energyBar.SetValue(newValueNormalized);
    }
}
