using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class EnergyReserve
{
    private int _startValue;
    private const int _baseMileage = 1;

    protected int CurrentValue;

    public bool HaveGas => CurrentValue >= 0;
    public float ValueNormalized => (float) CurrentValue / _startValue;

    public event Action<float> OnTankValueChange;

    public EnergyReserve(int startValue)
    {
        CurrentValue = _startValue = startValue;

        OnTankValueChange?.Invoke(ValueNormalized);
    }

    public virtual void SpendGas(int mileage = _baseMileage)
    {
        CurrentValue -= mileage;

        OnTankValueChange?.Invoke(ValueNormalized);
    }

    public virtual void AddGas(int count)
    {
        CurrentValue += count;
    }
}
