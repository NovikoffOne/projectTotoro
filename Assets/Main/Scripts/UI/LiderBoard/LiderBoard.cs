using Agava.YandexGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiderBoard :
    IEventReceiver<ClickLiderBoardButtonInMenu>,
    IEventReceiver<ClickGameActionEvent>
{
    public LiderBoard()
    {
        this.Subscribe<ClickLiderBoardButtonInMenu>();
    }

    ~LiderBoard()
    {
        this.Unsubscribe<ClickLiderBoardButtonInMenu>();
    }

    public void OnEvent(ClickLiderBoardButtonInMenu var)
    {
        //GP_Leaderboard.OnFetchSuccess += OnFetch;
        //GP_Leaderboard.OnFetchError += () => Debug.Log("Fetch Error");


        //GP_Leaderboard.Open("Liderboard");
        //GP_Leaderboard.Fetch("Liderboard");
        //Debug.Log("Open liderboards");
    }

    public void OnEvent(ClickGameActionEvent var)
    {
        if (var.GameAction == GameAction.Completed)
        {
            //GP_Player.AddScore(50);
            //GP_Player.Sync();
        }
    }

    //private void OnFetch(string tag, GP_Data data)
    //{
    //    var result = data.GetList<TestData>();

    //    result.ForEach(data => Debug.Log(data.test));

    //    GP_Leaderboard.OnFetchSuccess -= OnFetch;
    //}

}

[Serializable]

public class TestData
{
    public int test;
}
