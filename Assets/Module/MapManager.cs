using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private GridManager _grid;
    
    [SerializeField] private int _minNumberPassengersCarried;
    [SerializeField] private int _numberPassengersCarried = 0;

    private bool _canTransition => _numberPassengersCarried >= _minNumberPassengersCarried;
    
    private List<LandingPlace> _places;

    private void OnEnable()
    {
        StartCoroutine(DelayEnable());
    }

    private void OnDisable()
    {
        foreach (var place in _places)
        {
            place.PassengerChanged -= OnPassengerChanged;
        }
    }

    public void OnPassengerChanged()
    {
        ++_numberPassengersCarried;

        if (_canTransition)
            Debug.Log("Ворота открыты");
    }

    public void TransitionNewGrid()
    {
        if (_canTransition)
            Debug.Log("Новая локация"); //перемещение на новую локацию
        else
            Debug.Log("Вы еще не перевезли всех пассажиров");
    }
    
    private IEnumerator DelayEnable()
    {
        yield return new WaitForSeconds(.2f);

        _places = _grid.GetLandingList();

        foreach (var place in _places)
        {
            place.PassengerChanged += OnPassengerChanged;
        }
    }
}