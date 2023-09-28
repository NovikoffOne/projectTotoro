public readonly struct OnTankValueChange : IEvent
{
    public readonly float NewValue;

	public OnTankValueChange(float newValue)
	{
		NewValue = newValue;
	}
}
