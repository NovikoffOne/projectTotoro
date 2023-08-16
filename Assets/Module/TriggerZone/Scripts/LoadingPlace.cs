using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingPlace : MonoBehaviour, ITriggerZone
{
    [SerializeField] private int SiteLandingIndex = 0;
    [SerializeField] private Passenger _passengerPrefab;

    private Passenger _passenger;

    private void Start()
    {
        _passenger = Instantiate(_passengerPrefab, transform.position, Quaternion.identity);

        _passenger.Init(SiteLandingIndex);
    }

    public void ApplyEffect(Player player)
    {
        player.GetPassenger(_passenger);
        
        _passenger.Move(player.PassengerLoadingZone);

        _passenger = null;
    }
}
