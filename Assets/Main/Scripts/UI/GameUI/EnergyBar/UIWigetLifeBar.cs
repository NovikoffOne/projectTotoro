using UnityEngine;

public class UIWigetLifeBar : MonoBehaviour, IEventReceiver<OnTankValueChange>
{
    [SerializeField] private EnergyBar _energyBar;

    private void Awake()
    {
        EventBus.Subscribe(this);
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(this);
    }

    public void OnEvent(OnTankValueChange var)
    {
        _energyBar.SetValue(var.NewValue);
    }
}
