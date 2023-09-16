using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PoolPlayer<T> : PoolMono<T>
    where T : Player
{
    public PoolPlayer(T prefab) : base(prefab)
    {
    }

    public override void OnSpawn(T element)
    {
        element.transform.position = new Vector3(0, 0, 0);

        element.gameObject.SetActive(true);
    }

    public override void OnDespawn(T element)
    {
        element.gameObject.SetActive(false);
    }
}
