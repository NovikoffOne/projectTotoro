using IJunior.TypedScenes;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }

    [SerializeField] private List<GridManager> _gridPrefabs;
    [SerializeField] private Player _playerPrefab;

    [SerializeField] private GameObject _interLevelMenu;
    [SerializeField] private GameObject _gameOverPanel;
    
    [SerializeField] private int _minNumberPassengersCarried;
    [SerializeField] private int _numberPassengersCarried = 0;

    [SerializeField] private GridManager _grid;
    [SerializeField] private Player _player;

    private List<LandingPlace> _places;
    private LevelTransition _levelTransition;

    private bool _canTransition => _numberPassengersCarried >= _minNumberPassengersCarried;

    private void Start()
    {
        Init();
    }

    private void OnDisable()
    {
        foreach (var place in _places)
        {
            place.PassengerChanged -= OnEnergyChanged;
        }

        _levelTransition.PlayerInside -= OnPlayerInsaeded;
    }

    public void OpenInterLevelMenu()
    {
        Time.timeScale = 0;
        _interLevelMenu.SetActive(true);
    }

    private void Init()
    {
        _places = _grid.GetLandingPlaces;

        _levelTransition = _grid.GetLevelTransition;

        foreach (var place in _places)
        {
            place.PassengerChanged += OnEnergyChanged;
        }

        _player.OnGameOver += OnOpenGameOverPanel;

        _levelTransition.PlayerInside += OnPlayerInsaeded;
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

    private void OnEnergyChanged()
    {
        _numberPassengersCarried++;

        if (_canTransition)
        {
            Debug.Log("Ворота открыты");
        }
    }
}