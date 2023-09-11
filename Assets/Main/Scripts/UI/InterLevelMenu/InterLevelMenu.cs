using UnityEngine;

public class InterLevelMenu : MonoBehaviour
{
    public void OnButtonClickExit()
    {
        NewAction();

        IJunior.TypedScenes.MainMenu.Load();
    }

    public void OnButtonClickNextLevel()
    {
        NewAction();

        EventBus.Raise(new OnButtonClickPlay());
    }

    public void OnButtonClickPlay()
    {
        NewAction();
    }

    public void OnButtonClickReload()
    {
        NewAction();

        EventBus.Raise(new OnButtonClickReload());
    }

    public void OnButtonClickPause()
    {
        EventBus.Raise(new OnButtonClickPause());
    }

    public void NewAction()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);

        EventBus.Raise(new OnOpenMenu(true));
    }
}
