﻿public readonly struct PointsReturned : IEvent
{
    public readonly int Point;

    public PointsReturned(int point)
    {
        Point = point;
    }
}
