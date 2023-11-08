using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Threading;
using UnityEngine;

public class LevelStar :
    IEventReceiver<ClickGameActionEvent>
{
    private readonly MapManager MapManager;

    private float _startTime;

    public LevelStar(MapManager mapManager)
    {
        MapManager = mapManager;

        this.Subscribe<ClickGameActionEvent>();
    }

    private int LevelIndex => MapManager.GridIndex;

    public void OnEvent(ClickGameActionEvent var)
    {
        if(var.GameAction == GameAction.Completed)
        {
            PlayerPrefs.SetInt($"LevelPassed {LevelIndex}", 1);
         
            var currentData = CalculateStar(Time.time);

            Debug.Log($"@@@ Current Star = {currentData}");

            EventBus.Raise(new CalculateCountStar(currentData));

            if (PlayerPrefs.HasKey($"LevelStar {LevelIndex}"))
            {
                var oldData = PlayerPrefs.GetInt($"LevelStar {LevelIndex}");

                if (oldData < currentData)
                    PlayerPrefs.SetInt($"LevelStar {LevelIndex}", CalculateStar(Time.time));
            }
            else
                PlayerPrefs.SetInt($"LevelStar {LevelIndex}", CalculateStar(Time.time));

        }

        if(var.GameAction == GameAction.Start)
        {
            _startTime = Time.time;
        }
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
