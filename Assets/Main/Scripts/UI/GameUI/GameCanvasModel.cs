using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal class GameCanvasModel : IModel, 
    IEventReceiver<ClickGameActionEvent>,
    IEventReceiver<IsRewarded>,
    IEventReceiver<ChangeTutorialState>
{
    public GameCanvasModel()
    {
        this.Subscribe<ClickGameActionEvent>();
        this.Subscribe<IsRewarded>();
        this.Subscribe<ChangeTutorialState>();
    }

    public bool IsGameOver { get; private set; } = false;
    public bool IsCompleted { get; private set; } = false;
    public bool IsRewarded { get; private set; } = false;
    public int TutorialState { get; private set; }

    public void GetData()
    {
        
    }

    public void UpdateData()
    {
        IsGameOver = false;
        IsCompleted = false;
        IsRewarded = false;
    }

    public void PlayNextLevel()
    {
        EventBus.Raise(new ClickGameActionEvent(GameAction.ClickNextLevel));
    }

    public void Play()
    {
        EventBus.Raise(new ClickGameActionEvent(GameAction.ClickPlay));
    }

    public void Reload()
    {
        EventBus.Raise(new ClickGameActionEvent(GameAction.ClickReload));
    }

    public void Pause(bool isPause) 
    {
        Time.timeScale = isPause? 0 : 1;
        EventBus.Raise(new PlayerCanInput(!isPause)); 
    }

    public void Exit()
    {
        EventBus.Raise(new ClickGameActionEvent(GameAction.Exit));
    }

    public void OnEvent(ClickGameActionEvent var)
    {
        switch (var.GameAction)
        {
            case GameAction.Completed:
                IsCompleted = true;
                MVCConnecter.UpdateController<GameCanvasController>();
                Time.timeScale = 0;
                break;

            case GameAction.GameOver:
                IsGameOver = true;
                MVCConnecter.UpdateController<GameCanvasController>();
                Time.timeScale = 0;
                break;

            default:
                break;
        }
    }

    public void Unscribe()
    {
        this.Unsubscribe<ClickGameActionEvent>();
        this.Unsubscribe<IsRewarded>();
        this.Unsubscribe<ChangeTutorialState>();
    }

    public void Reward()
    {
        EventBus.Raise(new ClickGameActionEvent(GameAction.ClickReward));
    }

    public void OnEvent(IsRewarded var)
    {
        IsRewarded = true;
        MVCConnecter.UpdateController<GameCanvasController>();
    }

    public void OnEvent(ChangeTutorialState var)
    {
        TutorialState = var.TutorialState;
        MVCConnecter.UpdateController<GameCanvasController>();
    }

    public void SetActiveTutorialState(int state)
    {
        if(TutorialState < state)
        {
            TutorialState = state;
        }
    }
}