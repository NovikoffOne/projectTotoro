using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Timeline.TimelineAsset;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Animator _animator;

    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _durationMove = 1f;

    public Vector3 CurrentPosition { get; private set; }
    public Vector3 LastPosition { get; private set; }

    public event Action<Vector3> OnPositionChanged;

    private void Start()
    {
        OnPositionChanged += OnPositionChanged;

        _animator = GetComponentInChildren<Animator>();
    }

    private void OnDisable()
    {
        OnPositionChanged -= OnPositionChanged;
    }

    public void Move(Vector3 newPosition)
    {
        newPosition = ClampingMoveDirection(newPosition);

        LastPosition = CurrentPosition;

        CurrentPosition = newPosition;

        ChangePosition(CurrentPosition);

        OnPositionChanged?.Invoke(CurrentPosition);
    }

    private Vector2 ClampingMoveDirection(Vector2 newPosition)
    {
        float deltaX = newPosition.x - CurrentPosition.x;
        float deltaY = newPosition.y - CurrentPosition.y;

        newPosition.x = CurrentPosition.x + Mathf.Clamp(deltaX, -_speed, _speed);
        newPosition.y = CurrentPosition.y + Mathf.Clamp(deltaY, -_speed, _speed);

        return newPosition;
    }

    private void ChangePosition(Vector3 newPosition)
    {
        transform.DOMove(newPosition, _durationMove).SetEase(Ease.Linear, 0);
    }
}
