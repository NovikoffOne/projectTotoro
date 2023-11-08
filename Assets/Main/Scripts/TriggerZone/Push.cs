using UnityEngine;

public class Push : MonoBehaviour, ITriggerZone
{
    [SerializeField] private Vector3 _direction;

    public void ApplyEffect(Player player)
    {
        if (player.Movement.IsApplyAffect)
        {
            player.Movement.Move(player.Movement.CurrentPosition + _direction);

            EventBus.Raise<PlayerCanInput>(new PlayerCanInput(false));
        }
    }
}