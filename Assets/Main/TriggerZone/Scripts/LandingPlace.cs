using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingPlace : MonoBehaviour, ITriggerZone
{
    [SerializeField] private int _index;
    [SerializeField] private Transform _chargePostion;

    private Charge _charge;

    public event Action PassengerChanged;

    public void ApplyEffect(Player player)
    {
        _charge = player.ChargeChanger.GetCharge(_index);

        if (_charge == null)
            return;
        else
        {
            PassengerChanged?.Invoke();
            _charge.Move(_chargePostion);
        }
    }
}
