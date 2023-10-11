using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Timeline.TimelineAsset;

public class PlayerMovement
{
    private readonly PlayerView PlayerView;

    private readonly float Speed;

    private Queue<Vector3> _positions = new Queue<Vector3>();

    public Vector3 CurrentPosition { get; private set; }
    public Vector3 LastPosition { get; private set; }

    public PlayerMovement(PlayerView playerView, float speed = 1f)
    {
        PlayerView = playerView;
        Speed = speed;
    }

    public void Move(Vector3 newPosition)
    {
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
}