using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeChanger : MonoBehaviour
{
    [SerializeField] private Charge _chargePrefab;
    [SerializeField] private Transform _chargePosition;

    private Charge _charge;

    public Transform TargetPosition { get => _chargePosition; }

    public Charge InstantiateCharge()
    {
        _charge = Instantiate(_chargePrefab, TargetPosition.position, Quaternion.Euler(-90, 0, 0), TargetPosition);
        
        _charge.transform.position = TargetPosition.position;

        _charge.Init(0);

        return _charge;
    }

    public void SetCharge(Charge charge)
    {
        if(_charge != null)
            Destroy(_charge.gameObject);

        _charge = charge;
        
        _charge.Move(TargetPosition);
    }

    public Charge GetCharge(int index)
    {
        if (index == _charge.Index)
        {
            var charge = _charge;

            _charge = InstantiateCharge();
           
            return charge;
        }
        else
            return null;
    }
}
