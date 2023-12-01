using Assets.Main.Scripts.Events.GameEvents;
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

    public void OnEvent(OnTankValueChange gasCount)
    {
        _energyBar.SetValue(gasCount.NewValue);
    }
}
