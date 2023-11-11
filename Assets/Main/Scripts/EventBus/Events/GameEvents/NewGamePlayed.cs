public readonly struct NewGamePlayed : IEvent
{
    public readonly int IndexLevel;

    public NewGamePlayed(int index = 0)
    {
        IndexLevel = index;
    }
}
