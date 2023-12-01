using Assets.Main.Scripts.Events;
using Assets.Main.Scripts.Events.GameEvents;
using UnityEngine;

internal class GameCanvasModel : IModel,
    IEventReceiver<GameActionEvent>,
    IEventReceiver<IsRewarded>,
    IEventReceiver<TutorialStateChanged>,
    IEventReceiver<StarCalculated>,
    IEventReceiver<PointsReturned>
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
        EventBus.Raise(new PlayerCanInputed(!isPause));
        EventBus.Raise(new GameActionEvent(GameAction.Pause));
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
        //IsCompleted = true;

        //MVCConnecter.UpdateController<GameCanvasController>();
        //Time.timeScale = 0;
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
        this.Subscribe<TutorialStateChanged>();
        this.Subscribe<StarCalculated>();
        this.Subscribe<PointsReturned>();
    }

    public void Unscribe()
    {
        this.Unsubscribe<GameActionEvent>();
        this.Unsubscribe<IsRewarded>();
        this.Unsubscribe<TutorialStateChanged>();
        this.Unsubscribe<PointsReturned>();
        this.Unsubscribe<StarCalculated>();
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

    public void OnEvent(TutorialStateChanged var)
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
        EventBus.Raise(new TutorialComplted());
        IsTutorial = false;
    }

    public void OnEvent(PointsReturned var)
    {
        PlayerPoint = var.Point;
        IsCompleted = true;

        MVCConnecter.UpdateController<GameCanvasController>();
    }

    public void OnEvent(StarCalculated var)
    {
        CountStar = var.Count;
        IsCompleted = true;

        MVCConnecter.UpdateController<GameCanvasController>();
    }
}