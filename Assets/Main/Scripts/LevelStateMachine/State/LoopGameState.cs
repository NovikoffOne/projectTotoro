using UnityEngine;

public class LoopGameState : BaseState<LevelStateMachine>,
    IEventReceiver<EnergyChanged>,
    IEventReceiver<PlayerInsided>,
    IEventReceiver<GameActionEvent>
{
    public override void Enter()
    {
        Time.timeScale = 1;

        this.Subscribe<EnergyChanged>();
        this.Subscribe<GameActionEvent>();
        this.Subscribe<PlayerInsided>();

        EventBus.Raise(new PlayerCanInputed(true));
    }

    public override void Exit()
    {
        EventBus.Raise(new PlayerCanInputed(false));
        
        this.Unsubscribe<EnergyChanged>();
        this.Unsubscribe<GameActionEvent>();
        this.Unsubscribe<PlayerInsided>();
    }

    public void OnEvent(EnergyChanged isChargeChanged)
    {
        if (isChargeChanged.IsChargeChange == false && Target.GridIndex == 0)
        {
            EventBus.Raise(new TutorialStateChanged(3));
        }

        if (isChargeChanged.IsChargeChange == true)
        {
            Target.SetNumberCarried(1);
        }

        if (Target.IsCanTransition)
        {
            EventBus.Raise(new OpenLevelTransition());

            if (Target.GridIndex == 0)
                EventBus.Raise(new TutorialStateChanged(5));
        }
    }

    public void OnEvent(PlayerInsided playerInsided)
    {
        if (Target.IsCanTransition)
        {
            Target.DespawnPlayer();
            EventBus.Raise(new GameActionEvent(GameAction.Completed));
        }
    }

    public void OnEvent(GameActionEvent gameAction)
    {
        switch (gameAction.GameAction)
        {
            case GameAction.Start:
                Target.StartTutorial();
                break;

            case GameAction.ClickReload:
                Target.ChangeNewLevelState(Target.GridIndex);
                break;

            case GameAction.ClickNextLevel:
                Target.ChangeNewLevelState(Target.GridIndex + 1);
                break;

            case GameAction.Exit:
                Target.DespawnPlayer();
                IJunior.TypedScenes.MainMenu.Load();
                break;

            default:
                break;
        }
    }
}
