using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class InstalizeState : BaseState<MapManager>
{
    public override void Enter()
    {
        Target.SubscribeAll();
        EventBus.Raise(new PlayerCanInputed(false));
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        base.Update();
    }
}
