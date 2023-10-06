using UnityEngine;

public class PlayerInput : 
    IEventReceiver<PlayerCanInput>,
    IEventReceiver<NewGame>
{
    private Player _player;

    private LayerMask _ignorLayer = 3;

    private bool _canInput = true;
    private bool _firstClick = true;

    private float _rayDistance = 20;

    public PlayerInput(Player player)
    {
        _player = player;

        this.Subscribe<PlayerCanInput>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _canInput == true)
            _player.Movement.Move(GetMouseColision());
    }

    public void OnEvent(PlayerCanInput var)
    {
        _canInput = var.IsCanInput;
    }

    private Vector2 GetMouseColision()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, _rayDistance, _ignorLayer) && hit.transform.TryGetComponent<Tile>(out Tile tile)
          && _player.Movement.CurrentPosition != tile.Position)
        {
            _player.EnergyTank.SpendGas();

            if (_firstClick)
            {
                EventBus.Raise(new StartGame());
                _firstClick = false;
            }
            
            return tile.Position;
        }
        else
            return _player.Movement.CurrentPosition;
    }

    public void OnEvent(NewGame var)
    {
        _firstClick = true;
    }
}
