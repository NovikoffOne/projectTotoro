public readonly struct PlayerCanInput : IEvent
{
    public readonly bool IsCanInput;

    public PlayerCanInput(bool canInput)
    {
        IsCanInput = canInput;
    }
}
