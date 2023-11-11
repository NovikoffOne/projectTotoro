using UnityEngine;

public class Barrier : MonoBehaviour, ITriggerZone
{
    public void ApplyEffect(Player player)
    {
        player.Movement.Move(player.Movement.LastPosition);

        EventBus.Raise<PlayerCanInputed>(new PlayerCanInputed(false));
        EventBus.Raise(new GameActionEvent(GameAction.GameOver));
    }
}
