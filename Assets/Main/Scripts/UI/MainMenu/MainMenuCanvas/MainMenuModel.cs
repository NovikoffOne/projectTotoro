using Assets.Main.Scripts.Events;
using Assets.Main.Scripts.Events.GameEvents;
using Assets.Main.Scripts.Events.MainMenuEvents;
using IJunior.TypedScenes;

public class MainMenuModel : IModel
{
    public void GetData() { }

    public void UpdateData() { }

    public void Play()
    {
        EventBus.Raise(new ButtonPlayInMenuClicked());
    }

    public void LiderboardButtonClick()
    {
        EventBus.Raise(new LiderBoardButtonClicked());
    }

    public void LevelButton(int index)
    {
        EventBus.Raise(new PlayerCanInputed(true));
        EventBus.Raise(new NewGamePlayed(index));

        SampleScene8x5.Load();
    }
}
