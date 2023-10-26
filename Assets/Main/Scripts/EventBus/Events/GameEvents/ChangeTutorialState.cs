public readonly struct ChangeTutorialState : IEvent
{
    public readonly int TutorialState;

    public ChangeTutorialState(int tutorialState)
    {
        TutorialState = tutorialState;
    }
}
