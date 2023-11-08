using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour, ITriggerZone
{
    public void ApplyEffect(Player player)
    {
        player.Movement.Move(player.Movement.LastPosition);
        EventBus.Raise<PlayerCanInput>(new PlayerCanInput(false));
        EventBus.Raise(new ClickGameActionEvent(GameAction.GameOver));
    }
}
