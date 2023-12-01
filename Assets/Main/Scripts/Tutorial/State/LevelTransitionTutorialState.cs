using Assets.Main.Scripts.Fsm;

public class LevelTransitionTutorialState : BaseState<Tutorial>
{
    public override void Enter()
    {
        Target.PointLight.transform.position = Target.State5LightPosition;
    }
}
