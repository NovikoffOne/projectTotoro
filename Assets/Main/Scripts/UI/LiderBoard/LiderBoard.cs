using Agava.YandexGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Net.Configuration;

public class LiderBoard :
    IEventReceiver<ClickGameActionEvent>
{
    public static LiderBoard Instance;

    public LiderBoard()
    {
        if (Instance == null)
            Instance = this;
        
        this.Subscribe<ClickGameActionEvent>();
    }

    ~LiderBoard()
    {
        this.Unsubscribe<ClickGameActionEvent>();
    }

    public void OnEvent(ClickGameActionEvent var)
    {
        if (var.GameAction == GameAction.Completed)
        {
            OnSetLeaderboardScore();
        }
    }

    private void OnSetLeaderboardScore()
    {
        Leaderboard.GetPlayerEntry("Score", (result) =>
        {
            Debug.Log("@@@Записать 100 очков");

            if (result == null)
            {
                Debug.Log("");
                return;
            }
            else
            {
                Debug.Log($"@@@ result {result.score + 100}");
                Leaderboard.SetScore("Score", result.score + 100);
            }
        });
    }

    public void OnClickLiderBoard(Action<int, string, int> drawPlayers)
    {
        Debug.Log("@@@ OnClickLiderBoard");
        
        Leaderboard.GetEntries("Score", (result) =>
        {
            for (int i = 0; i < result.entries.Length; i++)
            {
                var index = i;
                var entry = result.entries[index];
                var name = entry.player.publicName;

                if (string.IsNullOrEmpty(entry.player.publicName))
                    name = "Anonymous";

                drawPlayers?.Invoke(entry.rank, entry.player.publicName, entry.score);
            }
        },
        onErrorCallback: msg => Debug.Log(msg)
        ); ;
    }
}