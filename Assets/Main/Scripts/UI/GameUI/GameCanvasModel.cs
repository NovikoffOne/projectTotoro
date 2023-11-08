using UnityEngine;

internal class GameCanvasModel : IModel,
    IEventReceiver<GameActionEvent>,
    IEventReceiver<IsRewarded>,
    IEventReceiver<ChangeTutorialState>,
    IEventReceiver<CalculateCountStar>,
    IEventReceiver<ReturnPlayerPoints>
{
    public GameCanvasModel()
    {
        SubscribeAll();
    }

    public bool IsGameOver { get; private set; } = false;
    public bool IsCompleted { get; private set; } = false;
    public bool IsRewarded { get; private set; } = false;
    public bool IsTutorial { get; private set; } = false;
    public int TutorialState { get; private set; }

    public int PlayerPoint { get; private set; }
    public int CountStar { get; private set; }

    public void GetData()
    {

    }

    public void UpdateData()
    {
        IsGameOver = false;
        IsCompleted = false;
        IsRewarded = false;

        PlayerPoint = 0;
        CountStar = 0;
    }

    public void PlayNextLevel()
    {
        EventBus.Raise(new GameActionEvent(GameAction.ClickNextLevel));
    }

    public void Play()
    {
        EventBus.Raise(new GameActionEvent(GameAction.ClickPlay));
    }

    public void Reload()
    {
        EventBus.Raise(new GameActionEvent(GameAction.ClickReload));
    }

    public void Pause(bool isPause)
    {
        Time.timeScale = isPause ? 0 : 1;
        EventBus.Raise(new PlayerCanInput(!isPause));
    }

    public void Exit()
    {
        EventBus.Raise(new GameActionEvent(GameAction.Exit));
    }

    public void OnEvent(GameActionEvent var)
    {
        switch (var.GameAction)
        {
            case GameAction.Completed:
                ActionComplete();
                break;

            case GameAction.GameOver:
                ActionGameOver();
                break;

            default:
                break;
        }
    }

    public void ActionComplete()
    {
        IsCompleted = true;

        MVCConnecter.UpdateController<GameCanvasController>();
        Time.timeScale = 0;
    }

    public void ActionGameOver()
    {
        IsGameOver = true;
        IsTutorial = false;

        MVCConnecter.UpdateController<GameCanvasController>();
        Time.timeScale = 0;
    }

    public void SubscribeAll()
    {
        this.Subscribe<GameActionEvent>();
        this.Subscribe<IsRewarded>();
        this.Subscribe<ChangeTutorialState>();
        this.Subscribe<CalculateCountStar>();
        this.Subscribe<ReturnPlayerPoints>();
    }

    public void Unscribe()
    {
        this.Unsubscribe<GameActionEvent>();
        this.Unsubscribe<IsRewarded>();
        this.Unsubscribe<ChangeTutorialState>();
        this.Unsubscribe<ReturnPlayerPoints>();
        this.Unsubscribe<CalculateCountStar>();
    }

    public void Reward()
    {
        EventBus.Raise(new GameActionEvent(GameAction.ClickReward));
    }

    public void OnEvent(IsRewarded var)
    {
        IsRewarded = true;
        IsGameOver = false;
        MVCConnecter.UpdateController<GameCanvasController>();
    }

    public void OnEvent(ChangeTutorialState var)
    {
        if (var.IsTutorial == true)
        {
            IsTutorial = true;
            TutorialState = var.TutorialState;
            MVCConnecter.UpdateController<GameCanvasController>();
        }
    }

    public void SetActiveTutorialState(int state)
    {
        TutorialState = state;
        MVCConnecter.UpdateController<GameCanvasController>();
    }

    public void CompleteTutorial()
    {
        EventBus.Raise(new TutorialCompletd());
        IsTutorial = false;
    }

    public void OnEvent(ReturnPlayerPoints var)
    {
        PlayerPoint = var.Point;
        MVCConnecter.UpdateController<GameCanvasController>();
    }

    public void OnEvent(CalculateCountStar var)
    {
        CountStar = var.Count;
        MVCConnecter.UpdateController<GameCanvasController>();
    }
}