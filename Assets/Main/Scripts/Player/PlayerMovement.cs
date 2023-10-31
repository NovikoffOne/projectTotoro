using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Timeline.TimelineAsset;

public class PlayerMovement :
    IEventReceiver<ClickGameActionEvent>
{
    private readonly PlayerView PlayerView;

    private readonly float Speed;

    public Vector3 CurrentPosition { get; private set; }
    public Vector3 LastPosition { get; private set; }

    private bool _isGame;

    public PlayerMovement(PlayerView playerView, float speed = 1f)
    {
        PlayerView = playerView;
        Speed = speed;

        this.Subscribe<ClickGameActionEvent>();
    }

    ~PlayerMovement()
    {
        this.Unsubscribe<ClickGameActionEvent>();
    }

    public void Move(Vector3 newPosition)
    {
        if (_isGame == false)
            return;

        newPosition = ClampingMoveDirection(newPosition);

        LastPosition = CurrentPosition;

        CurrentPosition = newPosition;
        
        PlayerView.ChangePosition(CurrentPosition);
    }

    public void ResetPosition()
    {
        CurrentPosition = new Vector3(0, 0, 0);

        LastPosition = new Vector3(0, 0, 0);

        PlayerView.ResetPosition();
    }

    private Vector2 ClampingMoveDirection(Vector2 newPosition)
    {
        float deltaX = newPosition.x - CurrentPosition.x;
        float deltaY = newPosition.y - CurrentPosition.y;

        newPosition.x = CurrentPosition.x + Mathf.Clamp(deltaX, -Speed, Speed);
        newPosition.y = CurrentPosition.y + Mathf.Clamp(deltaY, -Speed, Speed);

        return newPosition;
    }

    public void OnEvent(ClickGameActionEvent var)
    {
        switch (var.GameAction)
        {
            case GameAction.Start:
                _isGame = true;
                break;

            case GameAction.Completed:
                _isGame = false;
                break;

            case GameAction.ClickNextLevel:
                _isGame = true;
                break;

            default:
                break;
        }
    }
}