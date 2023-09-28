using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal class GameCanvasController : BaseUpdateController<GameCanvas, GameCanvasModel>
{
    public override void UpdateView()
    {
        if (Model.IsGameOver)
        {
            View.GameOverPanel.gameObject.SetActive(true);
            Model.UpdateData();
        }

        if (Model.IsCompleted)
        {
            View.InterLevelPanel.gameObject.SetActive(true);
            Model.Pause(true);
            Model.UpdateData();
        }
    }

    public override void HidePanel()
    {
        for (int i = 0; i < View.Panels.Count; i++)
        {
            View.Panels[i].Buttons.ForEach(button => button.onClick.RemoveAllListeners());
        }

        View.PauseButton.onClick.RemoveAllListeners();
    }

    // Происходит основная линковка
    protected override void OnShow()
    {
        View.PauseMenuPanel.CloseButton.onClick.AddListener(() => Close(View.PauseMenuPanel.gameObject));
        View.PauseMenuPanel.ReloadButton.onClick.AddListener(() => Reload(View.PauseMenuPanel.gameObject));
        View.PauseMenuPanel.PlayButton.onClick.AddListener(() => Play(View.PauseMenuPanel.gameObject));
        
        View.InterLevelPanel.NewLevelButton.onClick.AddListener(() => PlayNextLevel(View.InterLevelPanel.gameObject));
        View.InterLevelPanel.ReloadButton.onClick.AddListener(() => Reload(View.InterLevelPanel.gameObject));
        View.InterLevelPanel.ExitMenuButton.onClick.AddListener(() => Exit(View.InterLevelPanel.gameObject));
        
        View.GameOverPanel.ReloadButton.onClick.AddListener(() => Reload(View.GameOverPanel.gameObject));
        View.GameOverPanel.ExitMenuButton.onClick.AddListener(() => Exit(View.GameOverPanel.gameObject));
        
        View.PauseButton.onClick.AddListener(Pause);
    }

    // Методы реагирующие на нажатия

    private void Play(GameObject panel)
    {
        Model.Play();
        Close(panel);
    }

    private void PlayNextLevel(GameObject panel)
    {
        Model.PlayNextLevel();
        Close(panel);
    }

    private void Close(GameObject panel)
    {
        Model.Pause(false);
        View.PauseButton.gameObject.SetActive(true);
        panel.SetActive(false);
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
}