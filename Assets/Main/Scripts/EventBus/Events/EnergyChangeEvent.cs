public readonly struct EnergyChangeEvent : IEvenet
{
    public readonly bool IsChargeChange;

    public EnergyChangeEvent(bool isChargeChange)
    {
        IsChargeChange = isChargeChange;
    }
}
