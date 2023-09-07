public readonly struct OnOpenMenu : IEvenet
{
    public readonly bool CanInput;

    public OnOpenMenu(bool canInput)
    {
        CanInput = canInput;
    }
}
