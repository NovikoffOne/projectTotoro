using UnityEngine;

public class LoopGameState : BaseState<MapManager>,
    IEventReceiver<GameActionEvent>,
    IEventReceiver<EnergyChanged>
{
    public override void Enter()
    {
        this.Subscribe<EnergyChanged>();

        if (Time.timeScale == 0)
            Time.timeScale = 1;

        EventBus.Raise(new PlayerCanInputed(true));
    }

    public override void Exit()
    {
        EventBus.Raise(new PlayerCanInputed(false));
    }

    public void OnEvent(GameActionEvent gameAction)
    {
        //switch (gameAction.GameAction)
        //{
        //    case GameAction.GameOver:
        //        Target.StateMachine.ChangeState<OpenPanelState>(state => state.Target = Target);
        //        break;

        //    case GameAction.Start:
        //        Target.ChangeTutorialState();
        //        break;

        //    case GameAction.Pause:
        //        Target.StateMachine.ChangeState<OpenPanelState>(state => state.Target = Target);
        //        break;

        //    default:
        //        break;
        //}
    }

    public void OnEvent(EnergyChanged isChargeChanged)
    {
        Debug.Log($"isChargeChanged {isChargeChanged.IsChargeChange}");

        if (!isChargeChanged.IsChargeChange == false && Target.LevelIndex == 0)
        {
            EventBus.Raise(new TutorialStateChanged(3));
        }

        if (isChargeChanged.IsChargeChange == true)
        {
            Target.SetNumberCarried(1);
            Debug.Log(Target.NumberPassengersCarried);
        }

        if (Target.IsCanTransition)
        {
            EventBus.Raise(new OpenLevelTransition());

            Debug.Log("OpenLevelTransition");

            if (Target.LevelIndex == 0)
                EventBus.Raise(new TutorialStateChanged(5));
        }
    }

    //public void OnEvent(PlayerInsided var)
    //{
    //    if (Target.IsCanTransition)
    //    {
    //        Target.StateMachine.ChangeState<OpenPanelState>(state => state.Target = Target);
    //        EventBus.Raise(new GameActionEvent(GameAction.Completed));
    //        Target.DespawnPlayer();
    //    }
    //}
}
