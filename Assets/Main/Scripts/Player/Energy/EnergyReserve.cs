using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class EnergyReserve : 
    IEventReceiver<RewardAddGas>,
    IEventReceiver<GameActionEvent>
{
    private const int BaseMilage = 1;
    private const int PointMultiplier = 10;

    private int _startValue;

    protected int CurrentValue;

    public bool HaveGas => CurrentValue >= 0;
    public float ValueNormalized => (float) CurrentValue / _startValue;

    public EnergyReserve(int startValue)
    {
        CurrentValue = _startValue = startValue;

        this.Subscribe<RewardAddGas>();
        this.Subscribe<GameActionEvent>();
    }

    ~EnergyReserve()
    {
        this.Unsubscribe<RewardAddGas>();
        this.Unsubscribe<GameActionEvent>();
    }

    public virtual void SpendGas(int mileage = BaseMilage)
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

    public void OnEvent(RewardAddGas var)
    {
        AddGas(var.Value);
    }

    public void OnEvent(GameActionEvent var)
    {
        if(var.GameAction == GameAction.Completed)
        {
            var temp = CurrentValue * PointMultiplier;

            EventBus.Raise(new ReturnPlayerPoints(temp));
        }
    }
}
