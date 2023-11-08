using Agava.YandexGames;
using System;
using UnityEngine;

public class LiderBoard :
    IEventReceiver<ReturnPlayerPoints>
{
    public static LiderBoard Instance;

    private string _liderboardName = "Score";

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

    public void OnClickLiderBoard(Action<int, string, int> drawPlayers)
    {
        Leaderboard.GetEntries(_liderboardName, (result) =>
        {
            for (int i = 0; i < result.entries.Length; i++)
            {
                var entry = result.entries[i];
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
        Leaderboard.GetPlayerEntry(_liderboardName, (result) =>
        {
            if (result == null)
                return;
            else
                drawPlayer?.Invoke(result.rank, result.player.publicName, result.score);
        });
    }

    private void OnSetLeaderboardScore(int point)
    {
        Leaderboard.GetPlayerEntry(_liderboardName, (result) =>
        {
            if (result == null)
                Leaderboard.SetScore(_liderboardName, point);
            else
                Leaderboard.SetScore(_liderboardName, result.score + point);
        });
    }
}