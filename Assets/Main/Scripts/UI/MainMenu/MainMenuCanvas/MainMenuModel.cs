using Agava.YandexGames;
using UnityEngine.SceneManagement;

public class MainMenuModel : IModel
{
    public void GetData()
    {

    }

    public void UpdateData()
    {

    }

    public void Play()
    {
        EventBus.Raise(new ClickButtonPlayInMenu());
    }

    public void LiderboardButtonClick()
    {
        EventBus.Raise(new ClickLiderBoardButtonInMenu());
    }

    public void Settings()
    {
        EventBus.Raise(new ClickSettingsButtonInMenu());
    }

    public void LevelButton(int index)
    {
        EventBus.Raise(new PlayerCanInput(true));
        EventBus.Raise(new NewGame(index));
        SceneManager.LoadScene("SampleScene8x5");
    }
}
