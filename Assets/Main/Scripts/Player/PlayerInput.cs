using UnityEngine;

public class PlayerInput : IEventReceiver<OnOpenMenu>
{
    private Player _player;

    private LayerMask _ignorLayer = 3;

    private bool _canInput = true;

    private float _rayDistance = 20;

    public PlayerInput(Player player)
    {
        _player = player;

        EventBus.Subscribe((IEventReceiver<OnOpenMenu>)this);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _canInput == true)
            _player.Movement.Move(GetMouseColision());
    }

    public void OnEvent(OnOpenMenu var)
    {
        _canInput = var.CanInput;
    }

    private Vector2 GetMouseColision()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, _rayDistance, _ignorLayer) && hit.transform.TryGetComponent<Tile>(out Tile tile)
          && _player.Movement.CurrentPosition != tile.Position)
        {
            _player.EnergyTank.SpendGas();
            
            return tile.Position;
        }
        else
            return _player.Movement.CurrentPosition;
    }
}
