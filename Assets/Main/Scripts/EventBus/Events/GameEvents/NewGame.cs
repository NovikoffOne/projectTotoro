public readonly struct NewGame : IEvent
{
    public readonly int IndexLevel;

    public NewGame(int index = 0)
    {
        IndexLevel = index;
    }
}
