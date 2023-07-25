using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingPlace : MonoBehaviour, ITriggerZone
{
    [SerializeField] private int _index;

    private Passenger _passenger;

    public event Action PassengerChanged;

    public void ApplyEffect(Player player)
    {
        _passenger = player.LandPassenger(_index);

        if (_passenger == null)
            return;
        else
        {
            PassengerChanged?.Invoke();
            _passenger.Move(transform);
        }
    }
}
