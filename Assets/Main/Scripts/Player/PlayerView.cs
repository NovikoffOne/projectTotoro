using DG.Tweening;
using UnityEngine;
using static UnityEngine.Timeline.TimelineAsset;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Vector3 _startPosition = new Vector3(0, 0, -.3f);
    
    [SerializeField] private float _durationMove = 1f;

    //private Player _player;
    //private Animator _animator;

    private void Start()
    {
        Instantiate(prefab, _startPosition, Quaternion.identity, transform);
     //   _animator = GetComponent<Animator>();
     //   _player = GetComponent<Player>();
    }

    public void ChangePosition(Vector3 newPosition)
    {
        transform.DOMove(newPosition, _durationMove).SetEase(Ease.Linear, 0);
    }
}
