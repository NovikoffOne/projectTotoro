using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    [SerializeField] private ITriggerZone _entityTriggerZone;
    [SerializeField] private ZoneIndex _zoneIndex;

    public ZoneIndex ZoneIndex => _zoneIndex;

    private void Start()
    {
        _entityTriggerZone = GetComponent<ITriggerZone>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
            _entityTriggerZone.ApplyEffect(player);
    }
}
