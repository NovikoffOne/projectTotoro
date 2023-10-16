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
{
    [SerializeField] private Button _close;

    [SerializeField] private TMP_Text _myName;
    [SerializeField] private TMP_Text _myScore;
    [SerializeField] private TMP_Text _myRank;
    
    [SerializeField] private List<PlayerStringLiderboard> _playerStrings = new List<PlayerStringLiderboard>();

    private List<Button> _buttons = new List<Button>();

    public Button Close => _close;

    public List<Button> Buttons => _buttons;

    private void Start()
    {
        this.Subscribe<ClickLiderBoardButtonInMenu>();
        
        _buttons.Add(_close);
    
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        this.Unsubscribe<ClickLiderBoardButtonInMenu>();
    }

    public void OnEvent(ClickLiderBoardButtonInMenu var)
    {
        LiderBoard.Instance.OnClickLiderBoard(DrawLiders);
        LiderBoard.Instance.OnGetLeaderboardPlayerEntry(DrawPlayerRank);
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

