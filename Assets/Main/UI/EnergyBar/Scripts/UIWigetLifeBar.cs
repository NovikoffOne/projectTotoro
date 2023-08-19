using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWigetLifeBar : MonoBehaviour
{
    [SerializeField] private EnergyBar _energyBar;
    [SerializeField] private Tank _tank;

    private void OnEnable()
    {
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
