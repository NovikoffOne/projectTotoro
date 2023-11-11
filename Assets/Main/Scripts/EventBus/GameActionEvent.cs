public readonly struct GameActionEvent : IEvent
{
    public readonly GameAction GameAction;

    public GameActionEvent(GameAction gameAction)
    {
        GameAction = gameAction;
    }
}
