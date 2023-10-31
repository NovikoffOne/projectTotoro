using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public readonly struct CalculateCountStar : IEvent
{
    public readonly int Count;

    public CalculateCountStar(int count)
    {
        Count = count;
    }
}