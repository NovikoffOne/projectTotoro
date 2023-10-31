using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum GameAction
{
    None,
    ClickPlay,
    ClickReload,
    ClickNextLevel,
    Completed,
    GameOver,
    Exit,
    Start,
    ClickReward
}

public readonly struct ClickGameActionEvent : IEvent
{
    public readonly GameAction GameAction;
    //public readonly int LevelIndex;

    public ClickGameActionEvent(GameAction gameAction)
    {
        GameAction = gameAction;
        //LevelIndex = levelIndex;
    }
}
