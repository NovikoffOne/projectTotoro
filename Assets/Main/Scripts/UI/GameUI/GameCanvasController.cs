using TMPro;
using UnityEngine;

internal class GameCanvasController : BaseUpdateController<GameCanvas, GameCanvasModel>
{
    private const int StartState = 0;
    private const int State1 = 1;
    private const int State3 = 3;
    private const int State5 = 5;

    public override void UpdateView()
    {
        if (Model.IsGameOver)
        {
            View.GameOverPanel.gameObject.SetActive(true);
            View.PauseButton.gameObject.SetActive(false);

            Model.Pause(true);
            Model.UpdateData();
        }

        if (Model.IsCompleted)
        {
            View.PauseButton.gameObject.SetActive(false);

            View.InterLevelPanel.gameObject.SetActive(true);
            View.InterLevelPanel.StarFiller.FillStars(Model.CountStar, Model.PlayerPoint);

            Model.Pause(true);
            Model.UpdateData();
        }

        if (Model.IsRewarded)
            Close(View.GameOverPanel.gameObject);

        if (Model.IsTutorial == true)
        {
            View.TutorialPanel.Tutorial.gameObject.SetActive(true);

            SetActiveTutorialText(View.TutorialPanel.Texts[Model.TutorialState]);
        }
    }

    public override void HidePanel()
    {
        for (int i = 0; i < View.Panels.Count; i++)
            View.Panels[i].Buttons.ForEach(button => button.onClick.RemoveAllListeners());

        View.PauseButton.onClick.RemoveAllListeners();
        View.OnDestroyded -= HidePanel;

        Model.Unscribe();
    }

    protected override void OnShow()
    {
        View.OnDestroyded += HidePanel;

        View.TutorialPanel.NextButton.onClick.AddListener(NextButtonTutorial);

        View.PauseMenuPanel.ExitMenuButton.onClick.AddListener(() => Exit(View.PauseMenuPanel.gameObject));
        View.PauseMenuPanel.ReloadButton.onClick.AddListener(() => Reload(View.PauseMenuPanel.gameObject));
        View.PauseMenuPanel.PlayButton.onClick.AddListener(() => Play(View.PauseMenuPanel.gameObject));

        View.InterLevelPanel.NewLevelButton.onClick.AddListener(() => PlayNextLevel(View.InterLevelPanel.gameObject));
        View.InterLevelPanel.ReloadButton.onClick.AddListener(() => Reload(View.InterLevelPanel.gameObject));
        View.InterLevelPanel.ExitMenuButton.onClick.AddListener(() => Exit(View.InterLevelPanel.gameObject));

        View.GameOverPanel.ReloadButton.onClick.AddListener(() => Reload(View.GameOverPanel.gameObject));
        View.GameOverPanel.ExitMenuButton.onClick.AddListener(() => Exit(View.GameOverPanel.gameObject));
        View.GameOverPanel.RewardButton.onClick.AddListener(Reward);

        View.PauseButton.onClick.AddListener(Pause);
    }

    private void SetActiveTutorialText(TMP_Text text)
    {
        View.TutorialPanel.gameObject.SetActive(true);
        View.TutorialPanel.SetActiveText(text);

        Model.Pause(true);
    }

    private void NextButtonTutorial()
    {
        View.TutorialPanel.Texts.ForEach(text => text.gameObject.SetActive(false));

        Close(View.TutorialPanel.gameObject);

        if (ActiveNewTutorial(Model.TutorialState))
            Model.SetActiveTutorialState(Model.TutorialState + 1);
    }

    private bool ActiveNewTutorial(int state)
    {
        switch (state)
        {
            case State1:
            case State3:
            case StartState:
                return true;

            case State5:
                CompleteTutorial();
                return false;

            default:
                return false;
        }
    }

    private void Play(GameObject panel)
    {
        Model.Play();

        Close(panel);
    }

    private void CompleteTutorial()
    {
        Model.CompleteTutorial();

        View.TutorialPanel.Tutorial.gameObject.SetActive(false);
    }

    private void PlayNextLevel(GameObject panel)
    {
        Model.PlayNextLevel();

        Close(panel);
    }

    private void Close(GameObject panel)
    {
        Model.Pause(false);

        panel.SetActive(false);

        View.PauseButton.gameObject.SetActive(true);
    }

    private void Reload(GameObject panel)
    {
        Model.Reload();

        Close(panel);
    }

    private void Pause()
    {
        Model.Pause(true);

        View.PauseMenuPanel.gameObject.SetActive(true);
        View.PauseButton.gameObject.SetActive(false);
    }

    private void Exit(GameObject panel)
    {
        Model.Exit();

        Close(panel);
    }

    private void Reward()
    {
        Model.Reward();
    }
}