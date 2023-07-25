using DG.Tweening;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _durationMove = 1f;

    private void OnEnable()
    {
        _player.PositionChanged += OnPositionChanged;
    }

    private void OnDisable()
    {
        _player.PositionChanged -= OnPositionChanged;
    }

    public void OnPositionChanged(Vector3 newPosition)
    {
        transform.DOMove(newPosition, _durationMove);
    }
}
