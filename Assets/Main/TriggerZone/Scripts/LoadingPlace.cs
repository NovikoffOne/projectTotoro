using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingPlace : MonoBehaviour, ITriggerZone
{
    [SerializeField] private int SiteLandingIndex = 0;

    [SerializeField] private Transform _chargePosition;

    [SerializeField] private Charge _chargePrefab;

    private Charge _charge;

    private void Start()
    {
        _charge = Instantiate(_chargePrefab, _chargePosition.position, Quaternion.identity, transform);

        _charge.Init(SiteLandingIndex);
    }

    public void ApplyEffect(Player player)
    {
        player.ChargeChanger.SetCharge(_charge);
        
        _charge = null;
    }
}
