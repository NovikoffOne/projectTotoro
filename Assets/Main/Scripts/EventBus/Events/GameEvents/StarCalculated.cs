namespace Assets.Main.Scripts.Events.GameEvents
{
    public readonly struct StarCalculated : IEvent
    {
        public readonly int Count;

        public StarCalculated(int count)
        {
            Count = count;
        }
    }
}