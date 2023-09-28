using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal class GameCanvasModel : IModel, IEventReceiver<OpenGameOverPanel>
{
    public GameCanvasModel()
    {

    }

    public bool IsGameOver { get; private set; }

    public void GetData()
    {
        
    }

    public void UpdateData()
    {
        IsGameOver = false;
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

    public void OnEvent(OpenGameOverPanel var)
    {
        EventBus.Raise(new ClickGameActionEvent(GameAction.GameOver));
        IsGameOver = true;
        Time.timeScale = 0;
        MVCConnecter.UpdateController<GameCanvasController>();
    }
}