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

public readonly struct GameActionEvent : IEvent
{
    public readonly GameAction GameAction;

    public GameActionEvent(GameAction gameAction)
    {
        GameAction = gameAction;
    }
}
