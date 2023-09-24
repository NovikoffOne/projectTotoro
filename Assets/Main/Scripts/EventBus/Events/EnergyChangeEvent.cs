public readonly struct EnergyChangeEvent : IEvent
{
    public readonly bool IsChargeChange;

    public EnergyChangeEvent(bool isChargeChange)
    {
        IsChargeChange = isChargeChange;
    }
}
