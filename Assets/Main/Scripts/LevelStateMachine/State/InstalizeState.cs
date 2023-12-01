using Assets.Main.Scripts.Events;
using Assets.Main.Scripts.Fsm;

namespace Assets.Main.Scripts.LevelFSM
{
    public class InstalizeState : BaseState<LevelStateMachine>
    {
        public override void Enter()
        {
            EventBus.Raise(new PlayerCanInputed(false));
        }
    }
}