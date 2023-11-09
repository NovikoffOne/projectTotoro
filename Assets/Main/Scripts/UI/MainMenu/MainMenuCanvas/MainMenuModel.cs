using IJunior.TypedScenes;

public class MainMenuModel : IModel
{
    public void GetData() { }

    public void UpdateData() { }

    public void Play()
    {
        EventBus.Raise(new ClickButtonPlayInMenu());
    }

    public void LiderboardButtonClick()
    {
        EventBus.Raise(new ClickLiderBoardButtonInMenu());
    }

    public void LevelButton(int index)
    {
        EventBus.Raise(new PlayerCanInput(true));
        EventBus.Raise(new NewGame(index));

        SampleScene8x5.Load();
    }
}
