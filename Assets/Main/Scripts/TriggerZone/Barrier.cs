using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour, ITriggerZone
{
    public void ApplyEffect(Player player)
    {
        player.Movement.Move(player.Movement.LastPosition);
    }
}
