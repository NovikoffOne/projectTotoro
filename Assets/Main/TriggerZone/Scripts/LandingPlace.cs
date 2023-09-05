using System;
using UnityEngine;

public class LandingPlace : MonoBehaviour, ITriggerZone
{
    [SerializeField] private Charge _chargeRed;
    [SerializeField] private Transform _chargePostion;
    [SerializeField] private int _index;

    [SerializeField] private MapManager _mapManager; //

    private Charge _charge;

    public event Action PassengerChanged;

    public void Start()
    {
        _charge = Instantiate(_chargeRed, _chargePostion);
    }

    public void ApplyEffect(Player player)
    {
        var charge = player.ChargeChanger.GetCharge(_index);

        if (charge != null)
        {
            Destroy(_charge.gameObject);

            _charge = charge;

            //PassengerChanged?.Invoke();

            _mapManager.EventBus.Raise(new EnergyChangeEvent(true));

            _charge.Move(_chargePostion);
        }
        else
            throw new Exception("Не тот заряд!");
        
    }
}
