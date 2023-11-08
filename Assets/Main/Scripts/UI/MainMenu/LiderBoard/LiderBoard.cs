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
    IEventReceiver<ReturnPlayerPoints>
{
    public static LiderBoard Instance;

    public LiderBoard()
    {
#if !UNITY_WEBGL || UNITY_EDITOR

        return;
#endif
        if (Instance == null)
            Instance = this;
        
        this.Subscribe<ReturnPlayerPoints>();
    }

    ~LiderBoard()
    {
        this.Unsubscribe<ReturnPlayerPoints>();
    }

    public void OnEvent(ReturnPlayerPoints var)
    {
#if !UNITY_WEBGL || UNITY_EDITOR

        return;
#endif
        OnSetLeaderboardScore(var.Point);
    }

    
    private void OnSetLeaderboardScore(int point)
    {
        Leaderboard.GetPlayerEntry("Score", (result) =>
        {
            if (result == null)
                Leaderboard.SetScore("Score", point);
            else
                Leaderboard.SetScore("Score", result.score + point);
        });
    }

    public void OnClickLiderBoard(Action<int, string, int> drawPlayers)
    {
        Leaderboard.GetEntries("Score", (result) =>
        {
            for (int i = 0; i < result.entries.Length; i++)
            {
                var index = i;
                var entry = result.entries[index];
                var name = entry.player.publicName;

                if (string.IsNullOrEmpty(entry.player.publicName))
                    return;

                drawPlayers?.Invoke(entry.rank, entry.player.publicName, entry.score);
            }
        },
        onErrorCallback: msg => Debug.Log(msg)
        ); ;
    }

    public void OnGetLeaderboardPlayerEntry(Action<int, string, int> drawPlayer)
    {
        Leaderboard.GetPlayerEntry("Score", (result) =>
        {
            if (result == null)
                Debug.Log("@@@Player is not present in the leaderboard.");
            else
            {
                drawPlayer?.Invoke(result.rank, result.player.publicName, result.score);
            }
        });
    }
}