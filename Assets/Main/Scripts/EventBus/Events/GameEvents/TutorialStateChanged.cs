public readonly struct TutorialStateChanged : IEvent
{
    public readonly int TutorialState;
    public readonly bool IsTutorial;

    public TutorialStateChanged(int tutorialState, bool isTutorial = true)
    {
        TutorialState = tutorialState;
        IsTutorial = isTutorial;
    }
}
