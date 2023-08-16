using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerView : MonoBehaviour
{
    [SerializeField] private Passenger _passenger;
    [SerializeField] private float _durationMove = 2f;

    private void OnEnable()
    {
        _passenger.PositionChanged += OnPositionChanged;
    }

    private void OnDisable()
    {
        _passenger.PositionChanged -= OnPositionChanged;
    }

    public void OnPositionChanged(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
}
