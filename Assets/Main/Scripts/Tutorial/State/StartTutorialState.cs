using Assets.Main.Scripts.Fsm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Experimental.GlobalIllumination;

public class StartTutorialState : BaseState<Tutorial>
{
    public override void Enter()
    {
        if (Target.PointLight == null)
        {
            Target.CreatePointLigth();
            Target.PointLight.transform.position = Target.StartLightPosition;
        }
    }
}