using UnityEngine;

public class PlayerInput : 
    IEventReceiver<PlayerCanInput>,
    IEventReceiver<StartGame>
{
    private Player _player;
    private LayerMask _ignorLayer = 3;

    private bool _firstClick = true;
    private bool _canInput;

    private float _rayDistance = 20;

    public PlayerInput(Player player)
    {
        _player = player;

        this.Subscribe<PlayerCanInput>();
        this.Subscribe<StartGame>();
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

    public void OnEvent(StartGame var)
    {
        _firstClick = true;
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
                EventBus.Raise(new GameActionEvent(GameAction.Start));
                
                _firstClick = false;
            }
            
            return tile.Position;
        }
        else
            return _player.Movement.CurrentPosition;
    }
}
