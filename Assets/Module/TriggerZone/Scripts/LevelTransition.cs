using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransition : MonoBehaviour, ITriggerZone
{
    public event Action PlayerInside;

    public void ApplyEffect(Player player)
    {
        PlayerInside?.Invoke();
    }
}
