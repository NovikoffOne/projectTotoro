using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
public class NewLevelState : BaseState<LevelStateMachine>
{
    public override void Enter()
    {
        //if (Target.Player == null 
        //    || Target.Player.gameObject.activeSelf == false)
        //{
        //    Target.Player = Target.PlayerPool.Spawn();
        //}

        Target.Player = Target.PlayerPool.Spawn();

        if (Target.Data.GridData.Count > Target.GridIndex)
        {
            Target.Grid.NewLevel(Target.Data.GridData[Target.GridIndex]);
        }
        else
        {
            Target.PlayerPool.DeSpawn(Target.Player);
            IJunior.TypedScenes.MainMenu.Load();
        }

        EventBus.Raise(new PlayerCanInputed(false));

        Target.StateMachine.ChangeState<LoopGameState>(state => state.Target = Target);
    }
}
