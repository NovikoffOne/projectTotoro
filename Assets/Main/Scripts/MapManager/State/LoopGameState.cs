using UnityEngine;

public class LoopGameState : BaseState<LevelStateMachine>,
    IEventReceiver<EnergyChanged>
{
    public override void Enter()
    {
        Time.timeScale = 1;

        this.Subscribe<EnergyChanged>();

        EventBus.Raise(new PlayerCanInputed(true));
    }

    public override void Exit()
    {
        EventBus.Raise(new PlayerCanInputed(false));
        this.Unsubscribe<EnergyChanged>();
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
}
