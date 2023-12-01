using Assets.Main.Scripts.Events.GameEvents;
using Assets.Main.Scripts.PlayerEnity;
using UnityEngine;

namespace Assets.Main.Scripts.Generator
{
    public class LevelTransition : MonoBehaviour,
        ITriggerZone,
        IEventReceiver<OpenLevelTransition>

    {
        [SerializeField] private GameObject _door;
        [SerializeField] private Charge _particle;
        [SerializeField] private Transform _particlePosition;

        private void Start()
        {
            this.Subscribe();

            _particle = Instantiate(_particle, _particlePosition);

            _door.SetActive(false);
            _particle.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            _door.SetActive(false);
            _particle.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            this.Unsubscribe();
        }

        public void ApplyEffect(PlayerEnity.Player player)
        {
            EventBus.Raise(new PlayerInsided());
        }

        public void OnEvent(OpenLevelTransition var)
        {
            _door.SetActive(true);
            _particle.gameObject.SetActive(true);
        }
    }
}