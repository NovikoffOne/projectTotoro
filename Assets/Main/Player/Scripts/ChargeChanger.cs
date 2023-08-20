using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeChanger : MonoBehaviour
{
    [SerializeField] private Charge _chargePrefab;

    private Charge _charge;

    public Transform TargetPosition { get; private set; }

    public Charge InstantiateCharge()
    {
        TargetPosition = GetComponentInChildren<PlayerPrimitiv>().transform;
        _charge = Instantiate(_chargePrefab, TargetPosition.position, Quaternion.identity, TargetPosition);
        _charge.transform.position = TargetPosition.position;
        return _charge;
    }

    public void SetCharge(Charge charge)
    {
        var animator = GetComponentInChildren<Animator>();

        animator.SetTrigger("StayChanger");

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
