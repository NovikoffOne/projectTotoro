using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class InstalizeState : BaseState<LevelStateMachine>
{
    public override void Enter()
    {
        EventBus.Raise(new PlayerCanInputed(false));
    }
}
