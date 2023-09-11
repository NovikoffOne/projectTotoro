using DG.Tweening;
using UnityEngine;
using static UnityEngine.Timeline.TimelineAsset;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform _playerViewPosition;
    
    [SerializeField] private float _durationMove;

    private void Start()
    {
        Instantiate(prefab, _playerViewPosition.position, Quaternion.identity, transform);
    }

    public void ChangePosition(Vector3 newPosition)
    {
        transform.DOMove(newPosition, _durationMove).SetEase(Ease.Linear, 0);
    }
}