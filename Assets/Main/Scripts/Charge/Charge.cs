using System;
using UnityEngine;

public class Charge : MonoBehaviour
{
    private int _index;

    public int Index => _index;

    public event Action<Vector3> PositionChanged;

    public void Init(int index = 0)
    {
        _index = index;
    }

    public void Move(Transform transformParent)
    {
        transform.parent = transformParent;

        transform.position = transformParent.position;

        PositionChanged?.Invoke(transformParent.position);
    }
}
