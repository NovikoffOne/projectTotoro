public readonly struct EnergyChangeEvent : IEvenet
{
    public readonly bool IsPassengerChange;

    public EnergyChangeEvent(bool isPassengerChange)
    {
        IsPassengerChange = isPassengerChange;
    }
}
