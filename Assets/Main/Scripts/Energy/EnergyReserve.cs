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

    public EnergyReserve(int startValue)
    {
        CurrentValue = _startValue = startValue;

        EventBus.Raise(new OnTankValueChange(ValueNormalized));
    }

    public virtual void SpendGas(int mileage = _baseMileage)
    {
        CurrentValue -= mileage;

        EventBus.Raise(new OnTankValueChange(ValueNormalized));
    }

    public virtual void AddGas(int count)
    {
        CurrentValue += count;

        EventBus.Raise(new OnTankValueChange(ValueNormalized));
    }

    public virtual void SetValueReserve(int value)
    {
        CurrentValue = value;

        EventBus.Raise(new OnTankValueChange(ValueNormalized));
    }
}
