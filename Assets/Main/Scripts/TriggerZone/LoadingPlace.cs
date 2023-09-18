using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingPlace : MonoBehaviour, ITriggerZone
{
    [SerializeField] private ChargeColor _chargeColler;
    [SerializeField] private Transform _chargePosition;
    
    [SerializeField] private int SiteLandingIndex;

    private Charge _charge;

    private void OnEnable()
    {
        _charge = Instantiate(_chargeColler.GetMaterial(SiteLandingIndex), _chargePosition.position, Quaternion.identity, transform);
        
        _charge.Init(SiteLandingIndex);
    }

    private void OnDisable()
    {
        if (_charge != null)
            Destroy(_charge.gameObject);
    }

    public void ApplyEffect(Player player)
    {
        if(_charge != null)
        {
            player.ChargeChanger.SetCharge(_charge);

            _charge = null;
        }
    }
}