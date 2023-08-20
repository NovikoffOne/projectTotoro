using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingPlace : MonoBehaviour, ITriggerZone
{
    [SerializeField] private int SiteLandingIndex = 0;

    [SerializeField] private Transform _chargePosition;

    private Charge _charge;

    private ChargeColor _chargeColler;

    private void Awake()
    {
        _chargeColler = GetComponent<ChargeColor>();
    }

    private void Start()
    {
        _charge = Instantiate(_chargeColler.GetMaterial(SiteLandingIndex), _chargePosition.position, Quaternion.identity, transform);

        _charge.Init(SiteLandingIndex);
    }

    public void ApplyEffect(Player player)
    {
        player.ChargeChanger.SetCharge(_charge);
        
        _charge = null;
    }
}
