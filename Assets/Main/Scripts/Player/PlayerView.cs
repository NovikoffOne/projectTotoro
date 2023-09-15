using Assets.Main.Scripts.Test;
using DG.Tweening;
using UnityEngine;
using static UnityEngine.Timeline.TimelineAsset;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform _playerViewPosition;
    
    [SerializeField] private float _durationMove;

    private IStateMachine _stateMachine;

    private void Start()
    {
        Instantiate(prefab, _playerViewPosition.position, Quaternion.identity, transform);

        _stateMachine = new StateMachine();

        _stateMachine.ChangeState<MoveStateAnimation>(state => state.Target = this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            _stateMachine.ChangeState<MoveUpPlayerView>(state => state.Target = this);
            Debug.Log($"{_stateMachine.CurrentState.ToString()}");
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            _stateMachine.ChangeState<MoveStateAnimation>(state => state.Target = this);
            Debug.Log($"{_stateMachine.CurrentState.ToString()}");
        }
    }

    public void ChangePosition(Vector3 newPosition)
    {
        transform.DOMove(newPosition, _durationMove).SetEase(Ease.Linear, 0);
    }
}