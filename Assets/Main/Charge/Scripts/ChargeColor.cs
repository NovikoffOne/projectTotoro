using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeColor : MonoBehaviour
{
    [SerializeField] private List<Charge> _charges;

    public enum Color
    {
       Yellow = 0,
       Orange = 1,
       Red = 2,
       Pink = 3,
       Base = 4
    }

    public Charge GetMaterial(int index)
    {
        return _charges[index];
    }
}
