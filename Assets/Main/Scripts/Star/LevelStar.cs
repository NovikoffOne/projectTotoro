using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Threading;
using UnityEngine;

public class LevelStar :
    IEventReceiver<ClickGameActionEvent>,
    IEventReceiver<StartGame>
{
    private readonly MapManager MapManager;

    private float _startTime;
    

    public LevelStar(MapManager mapManager)
    {
        MapManager = mapManager;

        this.Subscribe<ClickGameActionEvent>();
        this.Subscribe<StartGame>();
    }
    private int LevelIndex => MapManager.GridIndex;

    public void OnEvent(ClickGameActionEvent var)
    {
        if(var.GameAction == GameAction.Completed)
        {
            PlayerPrefs.SetInt($"LevelPassed {LevelIndex}", 1);

            var oldData = PlayerPrefs.GetInt($"LevelStar {LevelIndex}");
            var currentData = CalculateStar(Time.time);

            if (oldData < currentData)
                PlayerPrefs.SetInt($"LevelStar {LevelIndex}", CalculateStar(Time.time));
        }
    }

    public void OnEvent(StartGame var)
    {
        _startTime = Time.time;
    }

    private int CalculateStar(float finishTime)
    {
        var time = finishTime - _startTime;

        if (time <= 30)
            return 3;

        if (time <= 40)
            return 2;
        
        if (time <= 50)
            return 1;

        if (time > 50)
            return 0;

        return 0;
    }
}
