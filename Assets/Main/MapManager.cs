using IJunior.TypedScenes;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }

    [SerializeField] private List<GridData> _gridData;
    [SerializeField] private Player _playerPrefab;

    [SerializeField] private GameObject _interLevelMenu;
    [SerializeField] private GameObject _gameOverPanel;

    [SerializeField] private GridManager _grid;
    [SerializeField] private Player _player;

    [SerializeField] private int _minNumberPassengersCarried;
    [SerializeField] private int _numberPassengersCarried = 0;

    [SerializeField] private int GridIndex;

    private List<LandingPlace> _places;
    private LevelTransition _levelTransition;

    private bool _canTransition => _numberPassengersCarried >= _minNumberPassengersCarried;

    private void Start()
    {
        _grid.NewGrid(_gridData[GridIndex]);

        Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _grid.NewGrid(_gridData[++GridIndex]);
        }
    }

    public void SetIndex(int index)
    {
        GridIndex = index;
    }

    private void OnDisable()
    {
        _places.ForEach(place => place.PassengerChanged -= OnEnergyChanged);

        _levelTransition.PlayerInside -= OnPlayerInsaeded;
    }

    public void OpenInterLevelMenu()
    {
        Time.timeScale = 0;
        _interLevelMenu.SetActive(true);
    }

    public void NewLevel()
    {
        _places.ForEach(place => place.PassengerChanged -= OnEnergyChanged);

        _levelTransition.PlayerInside -= OnPlayerInsaeded;

        _grid.NewGrid(_gridData[++GridIndex]);

        Init();
    }

    public void ReloadLevel()
    {
        _places.ForEach(place => place.PassengerChanged -= OnEnergyChanged);

        _levelTransition.PlayerInside -= OnPlayerInsaeded;

        _grid.NewGrid(_gridData[GridIndex]);

        Init();
    }

    private void Init()
    {
        _grid.GetLandingPlaces().ForEach(landingPlace => _places.Add(landingPlace.GetComponent<LandingPlace>()));

        Debug.Log(_places.Count);

        _levelTransition = _grid.GetLevelTransition;
        
        _places.ForEach(place => place.PassengerChanged += OnEnergyChanged);

        _player.OnGameOver += OnOpenGameOverPanel;
        _levelTransition.PlayerInside += OnPlayerInsaeded;
    }

    private void OnPlayerInsaeded()
    {
        if (_canTransition)
            OpenInterLevelMenu();
        else
            throw new System.Exception("Нет питания");
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