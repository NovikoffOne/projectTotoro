using UnityEngine;

namespace Assets.Main.Scripts.PlayerEnity
{
    public class PlayerMovement :
        IEventReceiver<GameActionEvent>
    {
        private readonly PlayerView PlayerView;

        private readonly float Speed;

        public PlayerMovement(PlayerView playerView, float speed = 1f)
        {
            PlayerView = playerView;
            Speed = speed;
            IsApplyAffect = true;

            this.Subscribe();
        }

        ~PlayerMovement()
        {
            this.Unsubscribe();
        }

        public Vector3 CurrentPosition { get; private set; }
        public Vector3 LastPosition { get; private set; }
        public bool IsApplyAffect { get; private set; }

        public void Move(Vector3 newPosition)
        {
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

        public void OnEvent(GameActionEvent gameAction)
        {
            if (gameAction.GameAction == GameAction.ClickReward)
            {
                IsApplyAffect = false;
            }
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
}