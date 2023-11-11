public readonly struct RewardGasAdded : IEvent
{
    public readonly int Value;

	public RewardGasAdded(int gasValue)
	{
		Value = gasValue;
	} 
}

