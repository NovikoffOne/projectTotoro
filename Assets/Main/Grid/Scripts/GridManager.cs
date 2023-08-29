using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public partial class GridManager : MonoBehaviour
{
    [SerializeField] private GridData _gridData;

    [SerializeField] private Tile _tilePrefab;

    [SerializeField] private Camera _camera;

    [SerializeField] private float _cameraPositionY;
    [SerializeField] private float _cameraPositionZ;

    [SerializeField] private Transform _tilePoolPosition;
    [SerializeField] private Transform _triggerZonePoolPosition;

    private PoolMono<Tile> _tilePool;

    private PoolMono<TriggerZone> _triggerZoneBarrier;
    private PoolMono<TriggerZone> _triggerZoneAccelerartor;
    private PoolMono<TriggerZone> _triggerZoneRepulsor;
    private PoolMono<TriggerZone> _triggerZoneLanding;
    private PoolMono<TriggerZone> _triggerZoneLoading;
    private PoolMono<TriggerZone> _triggerZoneLevelTransition;
   
    public LevelTransition GetLevelTransition => GetComponentInChildren<LevelTransition>();
    public List<LandingPlace> GetLandingPlaces => GetComponentsInChildren<LandingPlace>().ToList();

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        GeneraneGrid();
    }

    private void Start()
    {
        _camera.transform.position = new Vector3(((float)_gridData.Width / 2 - 0.5f), _cameraPositionY, _cameraPositionZ);
    }

    public void InstantiateGrid()
    {
        var countTile = 0;

        for (int x = 0; x < _gridData.Width; x++)
        {
            for (int y = 0; y < _gridData.Height; y++)
            {
                var isOffset = (countTile % 2 == 0);

                countTile++;

                var tempTile = _tilePool.Spawn();

                tempTile.transform.position = new Vector3(x, y, transform.position.z);

                tempTile.Init(isOffset);

                tempTile.name = $"Tile {x} {y}";

                tempTile.gameObject.SetActive(true);
            }
        }
    }

    private void GeneraneGrid()
    {
        _tilePool = new PoolMono<Tile>(_tilePrefab,  _tilePoolPosition);

        _triggerZoneBarrier = new PoolMono<TriggerZone>(_gridData.GetTriggerZone(ZoneIndex.Barier), _triggerZonePoolPosition);
        _triggerZoneAccelerartor = new PoolMono<TriggerZone>(_gridData.GetTriggerZone(ZoneIndex.Accelerartor), _triggerZonePoolPosition);
        _triggerZoneRepulsor = new PoolMono<TriggerZone>(_gridData.GetTriggerZone(ZoneIndex.Repulsor), _triggerZonePoolPosition);
        _triggerZoneLanding = new PoolMono<TriggerZone>(_gridData.GetTriggerZone(ZoneIndex.Landing), _triggerZonePoolPosition);
        _triggerZoneLoading = new PoolMono<TriggerZone>(_gridData.GetTriggerZone(ZoneIndex.Loading), _triggerZonePoolPosition);
        _triggerZoneLevelTransition = new PoolMono<TriggerZone>(_gridData.GetTriggerZone(ZoneIndex.LevelTransition), _triggerZonePoolPosition);

        InstantiateGrid();

        SetPositionsTriggerZones();
    }

    private void SetPositionsTriggerZones()
    {
        SetPositionTriggerZone(_gridData.BarriersPosition, _triggerZoneBarrier, _triggerZonePoolPosition);
        SetPositionTriggerZone(_gridData.AcceleratorPosition, _triggerZoneAccelerartor, _triggerZonePoolPosition);
        SetPositionTriggerZone(_gridData.RepulsorPosition, _triggerZoneRepulsor, _triggerZonePoolPosition);
        SetPositionTriggerZone(_gridData.LandingPlacePosition, _triggerZoneLanding, _triggerZonePoolPosition);
        SetPositionTriggerZone(_gridData.LoadingPlacePosition, _triggerZoneLoading, _triggerZonePoolPosition);
        SetPositionTriggerZone(_gridData.LevelTransitionPosition, _triggerZoneLevelTransition, _triggerZonePoolPosition);
    }

    private void SetPositionTriggerZone(IReadOnlyList<Vector3> zonePositions, PoolMono<TriggerZone> triggerZones, Transform poolContainer)
    {
        if (zonePositions != null && zonePositions.Count > 0)
        {
            for (int i = 0; i < zonePositions.Count; i++)
            {
                var triggerZone = triggerZones.Spawn();

                triggerZone.transform.position = zonePositions[i];

                triggerZone.name = $"{triggerZone.name} {triggerZone.transform.position.x} {triggerZone.transform.position.y}";
            }
        }
        else
            throw new System.NullReferenceException("список пуст");
        return;
    }
}
