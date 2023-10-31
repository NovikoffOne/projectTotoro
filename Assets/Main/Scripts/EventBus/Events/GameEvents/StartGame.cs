using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public readonly struct StartGame : IEvent
{

    public readonly int LevelIndex;

    public StartGame(int levelIndex = 0)
    {
        LevelIndex = levelIndex;
    }
}
