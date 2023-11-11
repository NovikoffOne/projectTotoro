using UnityEngine;

public class PlayerCharge : MonoBehaviour
{
    [SerializeField] private Charge _chargePrefab;
    [SerializeField] private Transform _chargePosition;

    private Charge _currentCharge;
    private Vector3 _quaternionRotate = new Vector3(-90, 0, 0);

    public Charge InstantiateCharge()
    {
        if (_currentCharge != null && _currentCharge.Index == 0)
            return _currentCharge;

        _currentCharge = Instantiate(_chargePrefab, TargetPosition.position, Quaternion.Euler(_quaternionRotate), TargetPosition);

        _currentCharge.transform.position = TargetPosition.position;

        _currentCharge.Init();

        return _currentCharge;
    }

    public Transform TargetPosition { get => _chargePosition; }

    public void SetCharge(Charge charge)
    {
        if (_currentCharge != null)
            Destroy(_currentCharge.gameObject);

        _currentCharge = charge;

        _currentCharge.Move(TargetPosition);
    }

    public Charge GetCharge(int index)
    {
        if (index == _currentCharge.Index)
        {
            var charge = _currentCharge;

            _currentCharge = InstantiateCharge();

            return charge;
        }
        else
            return null;
    }
}
