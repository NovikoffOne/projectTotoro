using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.LowLevel;

public class PlayerInMenuState : BaseState<LevelStateMachine>,
    IEventReceiver<NewGamePlayed>
{
    public override void Enter()
    {
        this.Subscribe<NewGamePlayed>();
    }

    public override void Exit()
    {
        this.Unsubscribe<NewGamePlayed>();
    }

    public void OnEvent(NewGamePlayed newLevelIndex)
    {
        ChangeNewLevelState(newLevelIndex.IndexLevel);
    }

    // Дубляж

    public void ChangeNewLevelState(int index = 0)
    {
        if (Target.Player != null)
        {
            Target.PlayerPool.DeSpawn(Target.Player);
        }

        Target.GridIndex = index;

        Target.StateMachine.ChangeState<NewLevelState>(state => state.Target = Target);
    }
}
