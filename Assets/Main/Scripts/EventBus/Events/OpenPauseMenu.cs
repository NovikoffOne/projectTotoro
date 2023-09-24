public readonly struct OpenPauseMenu : IEvent
{
    public readonly bool CanInput;

    public OpenPauseMenu(bool canInput)
    {
        CanInput = canInput;
    }
}
