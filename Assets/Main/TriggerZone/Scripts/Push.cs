using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour, ITriggerZone 
{
    [SerializeField] private Vector3 _direction;

    public void ApplyEffect(Player player)
    {
        player.Movement.Move(player.Movement.CurrentPosition + _direction);
    }
}
