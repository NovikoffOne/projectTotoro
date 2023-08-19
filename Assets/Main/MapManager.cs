using IJunior.TypedScenes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private GridManager _grid;

    [SerializeField] private Player _player;

    [SerializeField] private GameObject _interLevelMenu;
    [SerializeField] private GameObject _gameOverPanel;
    
    [SerializeField] private int _minNumberPassengersCarried;
    [SerializeField] private int _numberPassengersCarried = 0;

    private bool _canTransition => _numberPassengersCarried >= _minNumberPassengersCarried;
    
    private List<LandingPlace> _places;
    private LevelTransition _levelTransition;

    private void Start()
    {
        _places = _grid.GetLandingPlaces;

        _levelTransition = _grid.GetLevelTransition;

        foreach (var place in _places)
        {
            place.PassengerChanged += OnPassengerChanged;
        }

        _player.OnGameOver += OnOpenGameOverPanel;

        _levelTransition.PlayerInside += OnPlayerInsaeded;
    }

    private void OnDisable()
    {
        foreach (var place in _places)
        {
            place.PassengerChanged -= OnPassengerChanged;
        }

        _levelTransition.PlayerInside -= OnPlayerInsaeded;
    }

    public void OpenInterLevelMenu()
    {
        Time.timeScale = 0;
        _interLevelMenu.SetActive(true);
    }

    private void OnPlayerInsaeded()
    {
        if (_canTransition)
            OpenInterLevelMenu();
        else
            Debug.Log("Вы еще не перевезли всех пассажиров");
    }

    private void OnOpenGameOverPanel()
    {
        Time.timeScale = 0;
        _gameOverPanel.SetActive(true);
    }

    private void OnPassengerChanged()
    {
        ++_numberPassengersCarried;

        if (_canTransition)
        {
            Debug.Log("Ворота открыты");
        }
    }
}