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
    Start
}

public readonly struct ClickGameActionEvent : IEvent
{
    public readonly GameAction GameAction;
    public readonly int LevelIndex;

    public ClickGameActionEvent(GameAction gameAction, int levelIndex=0)
    {
        GameAction = gameAction;
        LevelIndex = levelIndex;
    }
}
