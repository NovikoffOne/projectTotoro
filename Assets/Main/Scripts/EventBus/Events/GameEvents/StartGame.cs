public readonly struct StartGame : IEvent
{

    public readonly int LevelIndex;

    public StartGame(int levelIndex = 0)
    {
        LevelIndex = levelIndex;
    }
}
