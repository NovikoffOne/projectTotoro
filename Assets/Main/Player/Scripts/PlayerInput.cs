using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Player _player;

    private LayerMask _ignorLayer = 3;

    private bool _isAlreadyInside = true;

    private float _rayDistance = 20;

    private void Start()
    {
        _player = GetComponent<Player>();   
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _isAlreadyInside == true)
        {
            _player.Movement.Move(GetMouseColision());
        }
    }

    private Vector2 GetMouseColision()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, _rayDistance, _ignorLayer) && hit.transform.TryGetComponent<Tile>(out Tile tile))
        {
            _player.EnergyTank.SpendGas();
            return tile.Position;
        }
        else
        {
            return _player.Movement.CurrentPosition;
        }
    }

    //private IEnumerator DelayLoadPassenger()
    //{
    //    _isAlreadyInside = false;

    //    yield return new WaitForSeconds(1f); // заменить на длительность анимации

    //    _isAlreadyInside = true;
    //}
}
