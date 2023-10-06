using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
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
        View.MenuPanel.PlayButton.onClick.AddListener(Play);
        View.MenuPanel.LiderBoardButton.onClick.AddListener(LiderBoard);
        View.MenuPanel.SettingsButton.onClick.AddListener(Settings);

        View.LiderBoardPanel.Close.onClick.AddListener(() => Close(View.LiderBoardPanel.gameObject));
        View.LevelSelectionPanel.CloseButton.onClick.AddListener(() => Close(View.LevelSelectionPanel.gameObject));

        for (var i = 0; i < View.LevelSelectionPanel.Buttons.Count; ++i)
        {
            var starsFillers = View.LevelSelectionPanel.GetComponentsInChildren<StarFiller>().ToList();

            var index = i;

            View.LevelSelectionPanel.Buttons[index].onClick.AddListener(() => LevelButton(index));
            View.LevelSelectionPanel.Buttons[index].GetComponentInChildren<TMP_Text>().text = $"{index + 1}";

            if (starsFillers.Count > i)
                starsFillers[index].FiilStars(index);
        }
    }

    private void Play()
    {
        Model.Play();
        View.LevelSelectionPanel.gameObject.SetActive(true);
        Close(View.MenuPanel.gameObject);
    }

    private void LevelButton(int index)
    {
        Model.LevelButton(index);
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