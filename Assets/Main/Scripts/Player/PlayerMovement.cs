using UnityEngine;

public class PlayerMovement :
    IEventReceiver<GameActionEvent>,
    IEventReceiver<StartGame>
{
    private readonly PlayerView PlayerView;

    private readonly float Speed;

    private bool _isGame;

    public PlayerMovement(PlayerView playerView, float speed = 1f)
    {
        PlayerView = playerView;
        Speed = speed;
        IsApplyAffect = true;

        this.Subscribe<GameActionEvent>();
    }

    ~PlayerMovement()
    {
        this.Unsubscribe<GameActionEvent>();
    }

    public Vector3 CurrentPosition { get; private set; }
    public Vector3 LastPosition { get; private set; }
    public bool IsApplyAffect { get; private set; }

    public void Move(Vector3 newPosition)
    {
        if (_isGame == false)
            return;

        IsApplyAffect = true;

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

        IsApplyAffect = true;
    }

    public void OnEvent(GameActionEvent var)
    {
        switch (var.GameAction)
        {
            case GameAction.Start:
                _isGame = true;
                break;

            case GameAction.Completed:
                _isGame = false;
                break;

            case GameAction.ClickNextLevel:
                _isGame = true;
                break;

            case GameAction.ClickReload:
                _isGame = true;
                break;

            case GameAction.ClickPlay:
                _isGame = true;
                break;

            case GameAction.Exit:
                _isGame = false;
                break;

            case GameAction.GameOver:
                _isGame = false;
                break;

            case GameAction.ClickReward:
                _isGame = true;
                IsApplyAffect = false;
                break;

            default:
                break;
        }
    }

    public void OnEvent(StartGame var)
    {
        _isGame = true;
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