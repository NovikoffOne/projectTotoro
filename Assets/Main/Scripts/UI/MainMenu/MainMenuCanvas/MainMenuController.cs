using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }

    private void Play()
    {
        Model.Play();
        Close();
    }

    private void LiderBoard()
    {
        Model.LiderBoard();
        Close();
    }

    private void Settings()
    {
        Model.Settings();
        Close();
    }

    private void Close()
    {
        View.MenuPanel.gameObject.SetActive(false);
    }


}
