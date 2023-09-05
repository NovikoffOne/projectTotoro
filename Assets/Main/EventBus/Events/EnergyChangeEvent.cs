using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public readonly struct EnergyChangeEvent : IEvenet
{
    public readonly bool IsPassengerChange;

    public EnergyChangeEvent(bool isPassengerChange)
    {
        IsPassengerChange = isPassengerChange;  
    }
}
