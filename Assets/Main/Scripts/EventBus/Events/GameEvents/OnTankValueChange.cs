namespace Assets.Main.Scripts.Events.GameEvents
{
    public readonly struct OnTankValueChange : IEvent
    {
        public readonly float NewValue;

        public OnTankValueChange(float newValue)
        {
            NewValue = newValue;
        }
    }
}