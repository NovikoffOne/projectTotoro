using UnityEngine;

public class LandingPlace : MonoBehaviour, ITriggerZone
{
    [SerializeField] private Charge _chargeRed;
    [SerializeField] private Transform _chargePostion;

    [SerializeField] private GameObject _basePlatform;
    [SerializeField] private GameObject _completPlatform;

    [SerializeField] private int _index;

    private Charge _charge;

    private void OnEnable()
    {
        _basePlatform.SetActive(true);
    }

    private void OnDisable()
    {
        if (_charge != null)
            Destroy(_charge.gameObject);
    }

    public void ApplyEffect(Player player)
    {
        var charge = player.ChargeChanger.GetCharge(_index);

        if (charge != null)
        {
            _charge = charge;

            EventBus.Raise(new EnergyChangeEvent(true));

            _charge.Move(_chargePostion);

            _basePlatform.SetActive(false);
            _completPlatform.SetActive(true);
        }
        else
            return;
    }
}
