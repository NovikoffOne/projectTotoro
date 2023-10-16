using Agava.YandexGames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class LiderBoardPanel : MonoBehaviour, 
    IPanel,
    IEventReceiver<ClickLiderBoardButtonInMenu>
    //IEventReceiver<ClickGameActionEvent>
{
    [SerializeField] private Button _close;

    //[SerializeField] private TMP_Text _name;
    //[SerializeField] private TMP_Text _score;
    //[SerializeField] private TMP_Text _rank;

    [SerializeField] private TMP_Text _myName;
    [SerializeField] private TMP_Text _myScore;
    [SerializeField] private TMP_Text _myRank;
    [SerializeField] private List<PlayerStringLiderboard> _playerStrings = new List<PlayerStringLiderboard>();

    private List<Button> _buttons = new List<Button>();

    //public TMP_Text PlayerName => _name;
    //public TMP_Text PlayerScore => _score;
    //public TMP_Text PlayerRank => _rank;
    public Button Close => _close;

    public List<Button> Buttons => _buttons;

    private void Start()
    {
        _buttons.Add(_close);
        this.Subscribe<ClickLiderBoardButtonInMenu>();
        //this.Subscribe<ClickGameActionEvent>();
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        this.Unsubscribe<ClickLiderBoardButtonInMenu>();
        //this.Unsubscribe<ClickGameActionEvent>();
    }

    public void OnEvent(ClickLiderBoardButtonInMenu var)
    {
        LiderBoard.Instance.OnClickLiderBoard(DrawLiders);
    }

    //public void OnEvent(ClickGameActionEvent var)
    //{
    //    if (var.GameAction == GameAction.Completed)
    //    {
    //        Debug.Log("Прибавить 100 очков");
    //        OnSetLeaderboardScoreButtonClick();
    //    }
    //}

    // Авторизация
    public void OnAuthorizeButtonClick()
    {
        PlayerAccount.Authorize();
    }

    // Запрос на разрешение использования личных данных
    public void OnRequestPersonalProfileDataPermissionButtonClick()
    {
        PlayerAccount.RequestPersonalProfileDataPermission();
    }

    // При нажатия кнопки Получить данные профиля
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

    //public void OnSetLeaderboardScoreButtonClick()
    //{
    //    Leaderboard.GetPlayerEntry("Score", (result) =>
    //    {
    //        Debug.Log("@@@Записать 100 очков");

    //        if (result == null)
    //        {
    //            Debug.Log("");
    //            return;
    //        }
    //        else
    //        {
    //            Debug.Log($"@@@ result {result.score + 100}");
    //            Leaderboard.SetScore("Score", result.score + 100);
    //        }
    //    });
    //}

    // При нажатии Установить счет в таблице лидеров
    public void OnGetLeaderboardEntriesButtonClick()
    {
        Leaderboard.GetEntries("Score", (result) =>
        {
            for (int i = 0; i < result.entries.Length; i++)
            {
                var index = i;
                var entry = result.entries[index];
                var name = entry.player.publicName;

                if (string.IsNullOrEmpty(entry.player.publicName))
                    name = "Anonymous";

                DrawLiders(index + 1, name, entry.score);
            }
        });
    }

    // Добавить игрока в таблицу лидеров
    public void OnGetLeaderboardPlayerEntryButtonClick()
    {
        Leaderboard.GetPlayerEntry("PlaytestBoard", (result) =>
        {
            if (result == null)
                Debug.Log("Player is not present in the leaderboard.");
            else
            {
                Debug.Log($"My rank = {result.rank}, score = {result.score}");
                DrawPlayerRank(result.rank, result.player.publicName, result.score);
            }
        });
    }

    private void OnAuthorizedInBackground()
    {
        Debug.Log($"{nameof(OnAuthorizedInBackground)} {PlayerAccount.IsAuthorized}");
    }

    private void DrawLiders(int rank, string name, int score)
    {
        _playerStrings[rank-1].PlayerRank.text = rank.ToString();
        _playerStrings[rank-1].PlayerName.text = name;
        _playerStrings[rank-1].PlayerScore.text = score.ToString();
    }

    private void DrawPlayerRank(int rank, string name, int score)
    {
        _myRank.text = rank.ToString();
        _myName.text = name;
        _myScore.text = score.ToString();
    }
}

