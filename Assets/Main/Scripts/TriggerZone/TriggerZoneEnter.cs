using UnityEngine;

namespace Assets.Main.Scripts.Generator
{
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
            if (other.TryGetComponent<PlayerEnity.Player>(out PlayerEnity.Player player))
                EntityTriggerZone.ApplyEffect(player);
        }
    }
}