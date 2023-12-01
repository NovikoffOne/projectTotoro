using Assets.Main.Scripts.Events;
using UnityEngine;

namespace Assets.Main.Scripts.Generator
{
    public class Barrier : MonoBehaviour, ITriggerZone
    {
        public void ApplyEffect(PlayerEnity.Player player)
        {
            player.Movement.Move(player.Movement.LastPosition);

            EventBus.Raise<PlayerCanInputed>(new PlayerCanInputed(false));
            EventBus.Raise(new GameActionEvent(GameAction.GameOver));
        }
    }
}