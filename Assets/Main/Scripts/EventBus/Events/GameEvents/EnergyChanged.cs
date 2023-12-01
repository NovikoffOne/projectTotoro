namespace Assets.Main.Scripts.Events.GameEvents
{
    public readonly struct EnergyChanged : IEvent
    {
        public readonly bool IsChargeChange;

        public EnergyChanged(bool isChargeChange)
        {
            IsChargeChange = isChargeChange;
        }
    }
}