using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
public class NewLevelState : BaseState<MapManager>
{
    public override void Enter()
    {
        if (Target.Player == null 
            || Target.Player.gameObject.activeSelf == false)
        {
            Target.SpawnPlayer();
        }

        Target.SetNumberCarried(0);

        if (Target.Data.GridData.Count > Target.GridIndex)
        {
            Target.Grid.NewLevel(Target.Data.GridData[Target.GridIndex]);
        }
        else
        {
            Target.DespawnPlayer();
            IJunior.TypedScenes.MainMenu.Load();
        }

        EventBus.Raise(new PlayerCanInputed(false));
        EventBus.Raise(new GameStarted(Target.GridIndex));

        Time.timeScale = 0;

        Target.StateMachine.ChangeState<LoopGameState>(state => state.Target = Target);
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        base.Update();
    }
}
