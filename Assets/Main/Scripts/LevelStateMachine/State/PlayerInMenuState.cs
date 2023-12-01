using Assets.Main.Scripts.Events.GameEvents;
using Assets.Main.Scripts.Fsm;

namespace Assets.Main.Scripts.LevelFSM
{
    public class PlayerInMenuState : BaseState<LevelStateMachine>,
        IEventReceiver<NewGamePlayed>
    {
        public override void Enter()
        {
            this.Subscribe();

            if (Target.Player != null)
            {
                Target.PlayerPool.DeSpawn(Target.Player);
            }
        }

        public override void Exit()
        {
            this.Unsubscribe();
        }

        public void OnEvent(NewGamePlayed newLevelIndex)
        {
            Target.GridIndex = newLevelIndex.IndexLevel;
            Target.StateMachine.ChangeState<NewLevelState>(state => state.Target = Target);
        }
    }
}