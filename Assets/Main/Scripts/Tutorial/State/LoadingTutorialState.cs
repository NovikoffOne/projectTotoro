public class LoadingTutorialState : BaseState<Tutorial>
{
    public override void Enter()
    {
        if(Target.PointLight != null)
        {
            Target.PointLight.transform.position = Target.State3LightPosition;
        }
    }
}
