using UnityEngine;

public class PlayerInput : 
    IEventReceiver<OpenPauseMenu>, 
    IEventReceiver<ClickGameActionEvent>
{
    private Player _player;

    private LayerMask _ignorLayer = 3;

    private bool _canInput = true;

    private float _rayDistance = 20;

    public PlayerInput(Player player)
    {
        _player = player;

        EventBus.Subscribe((IEventReceiver<OpenPauseMenu>)this);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _canInput == true)
            _player.Movement.Move(GetMouseColision());
    }

    public void OnEvent(OpenPauseMenu var)
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

    public void OnEvent(ClickGameActionEvent var) // Вот так лучше?
    {
        switch (var.GameAction)
        {
            case GameAction.Completed:
                _canInput = false;
                break;

            case GameAction.GameOver:
                _canInput = false;
                break;

            //case GameAction.Pause

            default:
                _canInput = true;
                break;
        }
    }
}
