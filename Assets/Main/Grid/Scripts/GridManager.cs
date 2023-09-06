using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public partial class GridManager : MonoBehaviour
{
    [SerializeField] private Tile _tilePrefab;

    [SerializeField] private Camera _camera;

    [SerializeField] private float _cameraPositionY;
    [SerializeField] private float _cameraPositionZ;

    [SerializeField] private Transform _tilePoolPosition;
    [SerializeField] private Transform _triggerZonePoolPosition;

    private GridData _gridData;
    
    private PoolMono<Tile> _tilePool;
    
    private List<PoolMono<TriggerZone>> _trigerZonePools = new List<PoolMono<TriggerZone>>();

    private PoolMono<TriggerZone> _triggerZoneBarrier;
    private PoolMono<TriggerZone> _triggerZoneAccelerartor;
    private PoolMono<TriggerZone> _triggerZoneRepulsor;
    private PoolMono<TriggerZone> _triggerZoneLanding;
    private PoolMono<TriggerZone> _triggerZoneLoading;
    private PoolMono<TriggerZone> _triggerZoneLevelTransition;

    public void Init(Camera camera)
    {
        _camera = camera;
    }

    public void NewGrid(GridData gridData)
    {
        _gridData = gridData;

        if (_trigerZonePools.Count > 0)
            _trigerZonePools.ForEach(pool => pool.DeSpawnAll());
        else
        {
            _triggerZoneBarrier = new PoolMono<TriggerZone>(_gridData.GetTriggerZone(ZoneIndex.Barier));
            _triggerZoneAccelerartor = new PoolMono<TriggerZone>(_gridData.GetTriggerZone(ZoneIndex.Accelerartor));
            _triggerZoneRepulsor = new PoolMono<TriggerZone>(_gridData.GetTriggerZone(ZoneIndex.Repulsor));
            _triggerZoneLanding = new PoolMono<TriggerZone>(_gridData.GetTriggerZone(ZoneIndex.Landing));
            _triggerZoneLoading = new PoolMono<TriggerZone>(_gridData.GetTriggerZone(ZoneIndex.Loading));
            _triggerZoneLevelTransition = new PoolMono<TriggerZone>(_gridData.GetTriggerZone(ZoneIndex.LevelTransition));

            _trigerZonePools.Add(_triggerZoneBarrier);
            _trigerZonePools.Add(_triggerZoneAccelerartor);
            _trigerZonePools.Add(_triggerZoneRepulsor);
            _trigerZonePools.Add(_triggerZoneLanding);
            _trigerZonePools.Add(_triggerZoneLoading);
            _trigerZonePools.Add(_triggerZoneLevelTransition);
        }

        if(_tilePool != null)
            _tilePool.DeSpawnAll();
        else
            _tilePool = new PoolMono<Tile>(_tilePrefab);

        GenerateGrid();
    }

    public List<TriggerZone> GetLandingPlaces()
    {
        var landingPlaces = new List<TriggerZone>();
        
        foreach (var landing in _triggerZoneLanding.Pool)
        {
            landingPlaces.Add(landing);
        }

        Debug.Log(landingPlaces.Count);

        return landingPlaces;
    }

    private void InstantiateGrid()
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
            }
        }
    }

    private void GenerateGrid()
    {
        InstantiateGrid();

        SetPositionsTriggerZones();

        _camera.transform.position = new Vector3(((float)_gridData.Width / 2 - 0.5f), _cameraPositionY, _cameraPositionZ);
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
