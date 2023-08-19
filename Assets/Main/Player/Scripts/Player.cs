using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CarTank _tank;
    
    [SerializeField] private Vector3 _startPosition = new Vector3(0, 0, -.3f);
    
    [SerializeField] private PlayerPrimitiv _playerPrefab;

    [SerializeField] private float _speed = 1f;

    [SerializeField] private Charge _chargePrefab;

    //private List<Passenger> _passengers = new List<Passenger>();

    public ChargeChanger ChargeChanger { get; private set; }

    private PlayerPrimitiv _primitive;

    private LayerMask _ignorLayer = 3;

    private bool _isAlreadyInside = true;

    private float _rayDistance = 20;
    
    public Vector3 CurrentPosition { get; private set; }
    public Vector3 LastPosition { get; private set; }

    public event Action<Vector3> OnPositionChanged;

    public event Action OnGameOver;

    private void Awake()
    {
        _primitive = Instantiate(_playerPrefab, _startPosition, Quaternion.identity, transform);
    }

    private void Start()
    {
        this.ChargeChanger = GetComponent<ChargeChanger>();

        this.ChargeChanger.InstantiateCharge(_chargePrefab);

        _tank.OnTankValueChange += OnValueChanged;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _isAlreadyInside == true)
        {
            Vector3 newPosition = ClampingMoveDirection(GetMouseColision());

            Move(newPosition);
        }
    }

    private void OnDisable()
    {
        _tank.OnTankValueChange -= OnValueChanged;
    }

    public Vector2 GetMouseColision()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, _rayDistance, _ignorLayer) && hit.transform.TryGetComponent<Tile>(out Tile tile))
        {
            _tank.SpendGas();
            return tile.Position;
        }
        else
        {
            return CurrentPosition;
        }
    }

    public void ResetPosition()
    {
        // Этот метод переделать под черную дыру
        //
        Move(Vector3.zero);
    }

    public void Rush(Vector3 direction)
    {
        Move(direction);
    }

    private void Move(Vector3 newPosition)
    {
        LastPosition = CurrentPosition;

        CurrentPosition = newPosition;

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

    //private IEnumerator DelayLoadPassenger()
    //{
    //    _isAlreadyInside = false;

    //    yield return new WaitForSeconds(1f); // заменить на длительность анимации

    //    _isAlreadyInside = true;
    //}

    private void OnValueChanged(float newValue)
    {
        if(_tank.HaveGas == false)
            OnGameOver?.Invoke();
    }
}
