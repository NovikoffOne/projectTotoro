public readonly struct GameStarted : IEvent
{

    public readonly int LevelIndex;

    public GameStarted(int levelIndex = 0)
    {
        LevelIndex = levelIndex;
    }
}
