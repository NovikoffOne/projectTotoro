using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public readonly struct StartGame : IEvent
{
	public readonly bool IsPlayGame; 

	public StartGame(bool isPlayGame)
	{
		IsPlayGame = isPlayGame;
	}
}
