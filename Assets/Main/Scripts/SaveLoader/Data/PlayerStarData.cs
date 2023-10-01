using System;

public class PlayerStarData : IData
{
    public int Count;
    public int LevelIndex;

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
