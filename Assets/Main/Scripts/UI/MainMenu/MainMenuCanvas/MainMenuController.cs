using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class MainMenuController : BaseController<MainMenuCanvas, MainMenuModel>
{
    public override void UpdateView()
    {
        Model.UpdateData();
    }

    public override void HidePanel()
    {
        View.MenuPanel.Buttons.ForEach(button => button.onClick.RemoveAllListeners());
    }

    protected override void OnShow()
    {
        View.MenuPanel.PlayButton.onClick.AddListener(() => Play());
        View.MenuPanel.LiderBoardButton.onClick.AddListener(() => LiderBoard());
        View.MenuPanel.SettingsButton.onClick.AddListener(() => Settings());

        View.LiderBoardPanel.Close.onClick.AddListener(() => Close(View.LiderBoardPanel.gameObject));

        View.LevelSelectionPanel.LevelButton.onClick.AddListener(() => LevelButton());
    }

    private void Play()
    {
        Model.Play();
        View.LevelSelectionPanel.gameObject.SetActive(true);
        Close(View.MenuPanel.gameObject);
    }

    private void LevelButton()
    {
        Model.LevelButton();
    }

    private void LiderBoard()
    {
        Model.LiderBoard();
        View.LiderBoardPanel.gameObject.SetActive(true);
        Close(View.MenuPanel.gameObject);
    }

    private void Settings()
    {
        Model.Settings();
        Close(View.MenuPanel.gameObject);
    }

    private void Close(GameObject panel)
    {
        View.MenuPanel.gameObject.SetActive(true);
        panel.SetActive(false);
    }
}