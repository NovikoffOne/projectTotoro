using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal class GameCanvasModel : IModel, 
    IEventReceiver<ClickGameActionEvent>
{
    public GameCanvasModel()
    {
        this.Subscribe<ClickGameActionEvent>();
    }

    public bool IsGameOver { get; private set; } = false;
    public bool IsCompleted { get; private set; } = false;

    public void GetData()
    {
        
    }

    public void UpdateData()
    {
        IsGameOver = false;
        IsCompleted = false;
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
        EventBus.Raise(new OpenPauseMenu(!isPause)); 
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
}