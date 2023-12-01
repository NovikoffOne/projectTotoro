using Assets.Main.Scripts.Fsm;

public class OffTutorialState : BaseState<Tutorial>
{
    public override void Enter()
    {
        if(Target.PointLight != null)
        {
            Target.PointLight.gameObject.SetActive(false);
        }

    }
}
