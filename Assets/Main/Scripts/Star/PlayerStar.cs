using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStar : MonoBehaviour
{
    public static PlayerStar Instance;

    private int _count;

    public int Count => _count;

    private void Awake()
    {
        Instance = this;
    }

    public void SetStar(int count) => this._count = count;

    public void OnAdded(int count) => _count += count;
}
