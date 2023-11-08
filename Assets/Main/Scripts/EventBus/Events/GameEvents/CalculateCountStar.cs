public readonly struct CalculateCountStar : IEvent
{
    public readonly int Count;

    public CalculateCountStar(int count)
    {
        Count = count;
    }
}