using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneEnter : MonoBehaviour
{
    [SerializeField] protected ZoneIndex Index;
    
    protected ITriggerZone EntityTriggerZone;

    public ZoneIndex ZoneIndex => Index;

    private void Start()
    {
        EntityTriggerZone = GetComponent<ITriggerZone>();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
            EntityTriggerZone.ApplyEffect(player);
    }
}
