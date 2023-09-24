using UnityEngine;

public class LevelTransition : MonoBehaviour, ITriggerZone
{
    public void ApplyEffect(Player player)
    {
        EventBus.Raise(new ClickGameActionEvent(GameAction.Completed));
    }
}