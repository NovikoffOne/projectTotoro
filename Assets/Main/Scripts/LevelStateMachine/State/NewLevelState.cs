using Assets.Main.Scripts.Events;
using Assets.Main.Scripts.Fsm;

namespace Assets.Main.Scripts.LevelFSM
{
    public class NewLevelState : BaseState<LevelStateMachine>
    {
        public override void Enter()
        {
            if (Target.Player != null)
            {
                Target.PlayerPool.DeSpawn(Target.Player);
            }

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
}