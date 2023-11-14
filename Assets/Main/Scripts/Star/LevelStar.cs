using UnityEngine;

public class LevelStar :
    IEventReceiver<GameActionEvent>
{
    private readonly LevelStateMachine MapManager;

    private const string LevelPassedText = "LevelPassed ";
    private const string LevelStarText = "LevelStar ";

    private const int TrueInt = 1;

    private const int MinTime = 30;
    private const int MidleTime = 40;
    private const int MaxTime = 50;

    private const int MaxCountStar = 3;
    private const int MidleCountStar = 2;
    private const int MinCountStar = 1;

    private float _startTime;

    public LevelStar(LevelStateMachine mapManager)
    {
        MapManager = mapManager;

        this.Subscribe<GameActionEvent>();
    }

    private int LevelIndex => MapManager.GridIndex;

    public void OnEvent(GameActionEvent var)
    {
        if (var.GameAction == GameAction.Completed)
        {
            PlayerPrefs.SetInt(LevelPassedText + LevelIndex, TrueInt);

            var currentData = CalculateStar(Time.time);

            EventBus.Raise(new StarCalculated(currentData));

            if (PlayerPrefs.HasKey(LevelStarText + LevelIndex))
            {
                var oldData = PlayerPrefs.GetInt(LevelStarText + LevelIndex);

                if (oldData < currentData)
                    PlayerPrefs.SetInt(LevelStarText + LevelIndex, CalculateStar(Time.time));
            }
            else
                PlayerPrefs.SetInt(LevelStarText + LevelIndex, CalculateStar(Time.time));
        }

        if (var.GameAction == GameAction.Start)
        {
            _startTime = Time.time;
        }
    }

    private int CalculateStar(float finishTime)
    {
        var time = finishTime - _startTime;

        if (time <= MinTime)
            return MaxCountStar;

        if (time <= MidleTime)
            return MidleCountStar;

        if (time <= MaxTime)
            return MinCountStar;

        if (time > MaxTime)
            return 0;

        return 0;
    }
}
