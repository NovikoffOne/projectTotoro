public readonly struct ChangeTutorialState : IEvent
{
    public readonly int TutorialState;
    public readonly bool IsTutorial;

    public ChangeTutorialState(int tutorialState, bool isTutorial = true)
    {
        TutorialState = tutorialState;
        IsTutorial = isTutorial;
    }
}
