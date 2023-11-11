using UnityEngine;

public class LevelTransition : MonoBehaviour,
    ITriggerZone,
    IEventReceiver<OpenLevelTransition>

{
    [SerializeField] private GameObject _door;
    [SerializeField] private Charge _particle;
    [SerializeField] private Transform _particlePosition;
    
    private void Start()
    {
        this.Subscribe<OpenLevelTransition>();

        _particle = Instantiate(_particle, _particlePosition);

        _door.SetActive(false);
        _particle.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _door.SetActive(false);
        _particle.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        this.Unsubscribe<OpenLevelTransition>();
    }

    public void ApplyEffect(Player player)
    {
        EventBus.Raise(new PlayerInsided());
    }

    public void OnEvent(OpenLevelTransition var)
    {
        _door.SetActive(true);
        _particle.gameObject.SetActive(true);
    }
}