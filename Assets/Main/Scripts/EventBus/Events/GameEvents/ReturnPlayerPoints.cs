public readonly struct ReturnPlayerPoints : IEvent
{
    public readonly int Point;

    public ReturnPlayerPoints(int point)
    {
        Point = point;
    }
}
