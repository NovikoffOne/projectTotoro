﻿public readonly struct PlayerCanInputed : IEvent
{
    public readonly bool IsCanInput;

    public PlayerCanInputed(bool canInput)
    {
        IsCanInput = canInput;
    }
}
