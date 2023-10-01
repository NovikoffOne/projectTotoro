using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public readonly struct NewGame : IEvent
{
    public readonly int IndexLevel;

	public NewGame(int index=0)
	{
		IndexLevel = index;
	}
}
