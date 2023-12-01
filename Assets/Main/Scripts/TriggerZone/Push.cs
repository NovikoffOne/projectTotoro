using Assets.Main.Scripts.Events;
using UnityEngine;

namespace Assets.Main.Scripts.Generator
{
    public class Push : MonoBehaviour, ITriggerZone
    {
        [SerializeField] private Vector3 _direction;

        public void ApplyEffect(PlayerEnity.Player player)
        {
            if (player.Movement.IsApplyAffect)
            {
                player.Movement.Move(player.Movement.CurrentPosition + _direction);

                EventBus.Raise(new PlayerCanInputed(false));
            }
        }
    }
}