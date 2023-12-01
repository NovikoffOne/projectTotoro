using Assets.Main.Scripts.Events;
using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Assets.Main.Scripts.PlayerEnity
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _playerViewPosition;

        [SerializeField] private float _durationMove;

        private void Start()
        {
            Instantiate(_prefab, _playerViewPosition.position, Quaternion.identity, transform);
        }

        public void ChangePosition(Vector3 newPosition)
        {
            transform.DOMove(newPosition, _durationMove).SetEase(Ease.Linear, 0);

            StartCoroutine(AllowInput());
        }

        public void ResetPosition()
        {
            transform.DOComplete();
            transform.position = new Vector3(0, 0, transform.position.z);
        }

        private IEnumerator AllowInput()
        {
            yield return new WaitForSeconds(_durationMove);

            EventBus.Raise(new PlayerCanInputed(true));
        }
    }
}