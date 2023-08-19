using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour
{
    public int Index { get; private set; }

    public event Action<Vector3> PositionChanged;

    public void Init(int index)
    {
        Index = index;
    }

    public void Move(Transform transformParent)
    {
        transform.parent = transformParent;

        transform.position = transformParent.position;

        PositionChanged?.Invoke(transformParent.position);
    }
}
