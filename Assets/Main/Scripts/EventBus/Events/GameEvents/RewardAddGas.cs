public readonly struct RewardAddGas : IEvent
{
    public readonly int Value;

	public RewardAddGas(int gasValue)
	{
		Value = gasValue;
	} 
}

