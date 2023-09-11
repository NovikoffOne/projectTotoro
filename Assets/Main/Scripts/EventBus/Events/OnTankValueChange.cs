public readonly struct OnTankValueChange : IEvenet
{
    public readonly float NewValue;

	public OnTankValueChange(float newValue)
	{
		NewValue = newValue;
	}
}
