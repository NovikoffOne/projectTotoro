using UnityEngine;

public class LoopGameState : BaseState<LevelStateMachine>,
    IEventReceiver<EnergyChanged>,
    IEventReceiver<PlayerInsided>,
    IEventReceiver<GameActionEvent>
{
    private bool _isChargeChanged;

    public override void Enter()
    {
        if(Target.Player == null || Target.Player.gameObject.activeSelf == false)
        {
            Target.PlayerPool.Spawn();
        }

        Time.timeScale = 1;

        this.Subscribe<EnergyChanged>();
        this.Subscribe<GameActionEvent>();
        this.Subscribe<PlayerInsided>();


        EventBus.Raise(new GameStarted(Target.GridIndex));
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
        _isChargeChanged = isChargeChanged.IsChargeChange;

        if (_isChargeChanged == false && Target.GridIndex == 0)
        {
            EventBus.Raise(new TutorialStateChanged(3));
        }

        if (_isChargeChanged == true)
        {
            EventBus.Raise(new OpenLevelTransition());

            if (Target.GridIndex == 0)
                EventBus.Raise(new TutorialStateChanged(5));
        }
    }

    public void OnEvent(PlayerInsided playerInsided)
    {
        if (_isChargeChanged)
        {
            Target.PlayerPool.DeSpawn(Target.Player);
            _isChargeChanged = false;

            EventBus.Raise(new GameActionEvent(GameAction.Completed));
        }
    }

    public void OnEvent(GameActionEvent gameAction)
    {
        switch (gameAction.GameAction)
        {
            case GameAction.Start:
                StartTutorial();
                break;

            case GameAction.ClickReload:
                ChangeNewLevelState(Target.GridIndex);
                break;

            case GameAction.ClickNextLevel:
                ChangeNewLevelState(Target.GridIndex + 1);
                break;

            case GameAction.Exit:
                Target.PlayerPool.DeSpawn(Target.Player);
                IJunior.TypedScenes.MainMenu.Load();
                break;

            default:
                break;
        }
    }

    public void StartTutorial()
    {
        if (Target.GridIndex == 0)
            EventBus.Raise(new TutorialStateChanged(0, true));
        else
            EventBus.Raise(new TutorialStateChanged(0, false));
    }

    public void ChangeNewLevelState(int index = 0)
    {
        //if (Target.Player != null)
        //{
        //    Target.PlayerPool.DeSpawn(Target.Player);
        //}

        Target.GridIndex = index;

        Target.StateMachine.ChangeState<NewLevelState>(state => state.Target = Target);
    }
}
