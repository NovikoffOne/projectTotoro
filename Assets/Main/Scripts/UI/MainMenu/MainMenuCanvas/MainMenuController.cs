using Agava.YandexGames;
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
    private bool _isNotAutorization = false;

    public override void UpdateView()
    {
        Model.UpdateData();
    }

    public override void HidePanel()
    {
        View.MenuPanel.Buttons.ForEach(button => button.onClick.RemoveAllListeners());
    }

    public void OnGetProfileDataButtonClick()
    {
        PlayerAccount.GetProfileData((result) =>
        {
            string name = result.publicName;

            if (string.IsNullOrEmpty(name))
                name = "Anonymous";

            Debug.Log($"My id = {result.uniqueID}, name = {name}");
        });
    }

    protected override void OnShow()
    {
        View.MenuPanel.PlayButton.onClick.AddListener(Play);

        View.MenuPanel.LiderBoardButton.onClick.AddListener(AuthorizePanel);

        View.AuthorizePanel.AuthorizeButton.onClick.AddListener(AutorizeButtonClick);
        View.AuthorizePanel.DontAuthorizeButton.onClick.AddListener(DontAutorizeButtonClick);

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

    private void LiderboardButtonClick()
    {
        if (View.AuthorizePanel.gameObject.activeSelf == true)
            View.AuthorizePanel.gameObject.SetActive(false);

        if (PlayerAccount.IsAuthorized == true)
        {
            LiderBoard.Instance.OnClickLiderBoard(DrawLiders);
            LiderBoard.Instance.OnGetLeaderboardPlayerEntry(DrawPlayerRank);

            Model.LiderboardButtonClick();
            View.LiderBoardPanel.gameObject.SetActive(true);
            Close(View.MenuPanel.gameObject);
        }
    }

    private void AuthorizePanel()
    {
        if (PlayerAccount.IsAuthorized == false && _isNotAutorization == false)
        {
            View.AuthorizePanel.gameObject.SetActive(true);
            Close(View.MenuPanel.gameObject);
        
            return;
        }

        if (PlayerAccount.IsAuthorized == true)
        {
            LiderboardButtonClick();
            Close(View.MenuPanel.gameObject);
        }

        if(PlayerAccount.IsAuthorized == false && _isNotAutorization == true)
        {
            return;
        }
    }

    private void AutorizeButtonClick()
    {
        PlayerAccount.Authorize(() => PlayerAccount.RequestPersonalProfileDataPermission(), 
            (msg) => Debug.Log($"@@@  PlayerAccount.Authorize == Error"));

        Close(View.AuthorizePanel.gameObject);
    }

    private void DontAutorizeButtonClick()
    {
        _isNotAutorization = true;
        Close(View.AuthorizePanel.gameObject);
    }

    private void Close(GameObject panel)
    {
        View.MenuPanel.gameObject.SetActive(true);
        panel.SetActive(false);
    }

    private void DrawLiders(int rank, string name, int score)
    {
        View.LiderBoardPanel.PlayersStrings[rank - 1].gameObject.SetActive(true);

        View.LiderBoardPanel.PlayersStrings[rank-1].PlayerRank.text = rank.ToString();
        View.LiderBoardPanel.PlayersStrings[rank-1].PlayerName.text = name;
        View.LiderBoardPanel.PlayersStrings[rank-1].PlayerScore.text = score.ToString();
    }

    private void DrawPlayerRank(int rank, string name, int score)
    {
        View.LiderBoardPanel.CurrentPlayerScore.gameObject.SetActive(true);

        View.LiderBoardPanel.CurrentPlayerScore.PlayerRank.text = rank.ToString();
        View.LiderBoardPanel.CurrentPlayerScore.PlayerName.text = name;
        View.LiderBoardPanel.CurrentPlayerScore.PlayerScore.text = score.ToString();
    }
}