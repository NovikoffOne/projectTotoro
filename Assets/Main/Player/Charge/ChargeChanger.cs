using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeChanger : MonoBehaviour
{
    [SerializeField] private Charge _chargePrefab;

    private Charge _charge;

    public Transform TargetPosition { get; private set; }

    private void Start()
    {
        TargetPosition = GetComponentInChildren<PlayerPrimitiv>().transform;
    }

    public Charge InstantiateCharge(Charge charge)
    {
        _charge = Instantiate(charge, TargetPosition.position, Quaternion.identity, TargetPosition);
        _charge.transform.position = TargetPosition.position;
        return _charge;
    }

    public void SetCharge(Charge charge)
    {
        Destroy(_charge.gameObject);

        _charge = charge;
        
        _charge.Move(TargetPosition);
    }

    public Charge GetCharge(int index)
    {
        if (index == _charge.Index)
        {
            var charge = _charge;

            _charge = InstantiateCharge(_chargePrefab);
           
            return charge;
        }
        else
            return null;
    }
}
