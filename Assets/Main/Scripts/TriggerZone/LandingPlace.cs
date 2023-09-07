using System;
using Unity.VisualScripting;
using UnityEngine;

public class LandingPlace : MonoBehaviour, ITriggerZone
{
    [SerializeField] private Charge _chargeRed;
    [SerializeField] private Transform _chargePostion;
    [SerializeField] private int _index;

    private Charge _charge;

    private void OnEnable()
    {
        _charge = Instantiate(_chargeRed, _chargePostion);
    }

    private void OnDisable()
    {
        if (_charge != null)
            Destroy(_charge.gameObject);
    }

    public void ApplyEffect(Player player)
    {
        var charge = player.ChargeChanger.GetCharge(_index);

        if (charge != null)
        {
            Destroy(_charge.gameObject);

            _charge = charge;

            EventBus.Raise(new EnergyChangeEvent(true));

            _charge.Move(_chargePostion);
        }
        else
            throw new Exception("Не тот заряд!");
    }
}
