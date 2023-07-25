using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Vector3 _startPosition = new Vector3(0, 0, -.3f);
    [SerializeField] private float _speed = 1f;

    [SerializeField] private GameObject _playerPrefab;

    private List<Passenger> _passengers = new List<Passenger>();

    private LayerMask _ignorLayer = 3;

    private bool _isAlreadyInside = true;

    private float _rayDistance = 20;

    public Vector3 CurrentPosition { get; private set; }

    public event Action<Vector3> PositionChanged;

    private void Start()
    {
        Instantiate(_playerPrefab, _startPosition, Quaternion.identity, transform);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _isAlreadyInside == true)
        {
            Vector3 newPosition = ClampingMoveDirection(GetMouseColision());

            Move(newPosition);
        }
    }

    public Vector2 GetMouseColision()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, _rayDistance, _ignorLayer) && hit.transform.TryGetComponent<Tile>(out Tile tile))
            return tile.Position;
        else
            return CurrentPosition;
    }

    public void ResetPosition()
    {
        Move(Vector3.zero);
    }

    public void Rush(Vector3 direction)
    {
        Move(CurrentPosition + direction);
    }

    public void GetPassenger(Passenger passenger)
    {
        Debug.Log(passenger);

        _passengers.Add(passenger);
    }

    public Passenger LandPassenger(int index)
    {
        foreach (var passenger in _passengers)
        {
            if (passenger.Index == index)
            {
                Debug.Log(passenger.Index + " вышел из автоуса");

                _passengers.Remove(passenger);

                StartCoroutine(DelayLoadPassenger());
                
                return passenger;
            }
        }

        return null;
    }

    private void Move(Vector3 newPosition)
    {
        CurrentPosition = newPosition;

        PositionChanged?.Invoke(CurrentPosition);
    }

    private Vector2 ClampingMoveDirection(Vector2 newPosition)
    {
        float deltaX = newPosition.x - CurrentPosition.x;
        float deltaY = newPosition.y - CurrentPosition.y;

        newPosition.x = CurrentPosition.x + Mathf.Clamp(deltaX, -_speed, _speed);
        newPosition.y = CurrentPosition.y + Mathf.Clamp(deltaY, -_speed, _speed);

        return newPosition;
    }

    private IEnumerator DelayLoadPassenger()
    {
        _isAlreadyInside = false;

        yield return new WaitForSeconds(1f);

        _isAlreadyInside = true;
    }
}
