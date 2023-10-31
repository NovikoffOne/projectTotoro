using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public partial class GridGenerator
{
    private GridData _gridData;
    private PoolMono<Tile> _tilePool;
    
    private List<PoolMono<TriggerZoneEnter>> _trigerZonePools = new List<PoolMono<TriggerZoneEnter>>();

    private PoolMono<TriggerZoneEnter> _triggerZoneBarrier;
    private PoolMono<TriggerZoneEnter> _triggerZoneAccelerartor;
    private PoolMono<TriggerZoneEnter> _triggerZoneRepulsor;
    private PoolMono<TriggerZoneEnter> _triggerZoneLanding;
    private PoolMono<TriggerZoneEnter> _triggerZoneLoading;
    private PoolMono<TriggerZoneEnter> _triggerZoneLevelTransition;

    public void NewGrid(GridData gridData)
    {
        _gridData = gridData;

        if (_trigerZonePools.Count > 0)
            _trigerZonePools.ForEach(pool => pool.DeSpawnAll());
        else
        {
            _triggerZoneBarrier = new PoolMono<TriggerZoneEnter>(_gridData.GetTriggerZone(ZoneIndex.Barier));
            _triggerZoneAccelerartor = new PoolMono<TriggerZoneEnter>(_gridData.GetTriggerZone(ZoneIndex.Accelerartor));
            _triggerZoneRepulsor = new PoolMono<TriggerZoneEnter>(_gridData.GetTriggerZone(ZoneIndex.Repulsor));
            _triggerZoneLanding = new PoolMono<TriggerZoneEnter>(_gridData.GetTriggerZone(ZoneIndex.Landing));
            _triggerZoneLoading = new PoolMono<TriggerZoneEnter>(_gridData.GetTriggerZone(ZoneIndex.Loading));
            _triggerZoneLevelTransition = new PoolMono<TriggerZoneEnter>(_gridData.GetTriggerZone(ZoneIndex.LevelTransition));

            _trigerZonePools.Add(_triggerZoneBarrier);
            _trigerZonePools.Add(_triggerZoneAccelerartor);
            _trigerZonePools.Add(_triggerZoneRepulsor);
            _trigerZonePools.Add(_triggerZoneLanding);
            _trigerZonePools.Add(_triggerZoneLoading);
            _trigerZonePools.Add(_triggerZoneLevelTransition);
        }

        if (_tilePool != null)
            _tilePool.DeSpawnAll();
        else
            _tilePool = new PoolMono<Tile>(_gridData.TilePrefab);

        GenerateGrid();
    }

    public List<TriggerZoneEnter> GetLandingPlaces()
    {
        var landingPlaces = new List<TriggerZoneEnter>();
        
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

                tempTile.transform.position = new Vector3(x, y, 0);

                tempTile.Init(isOffset);

                tempTile.name = $"Tile {x} {y}";
            }
        }
    }

    private void GenerateGrid()
    {
        InstantiateGrid();

        SetPositionsTriggerZones();

        Camera.main.transform.position = new Vector3(((float)_gridData.Width / 2 - 0.5f), _gridData.CameraPositionY, _gridData.CameraPositionZ);
    }

    private void SetPositionsTriggerZones()
    {
        SetPositionTriggerZone(_gridData.BarriersPosition, _triggerZoneBarrier);
        SetPositionTriggerZone(_gridData.AcceleratorPosition, _triggerZoneAccelerartor);
        SetPositionTriggerZone(_gridData.RepulsorPosition, _triggerZoneRepulsor);
        SetPositionTriggerZone(_gridData.LandingPlacePosition, _triggerZoneLanding);
        SetPositionTriggerZone(_gridData.LoadingPlacePosition, _triggerZoneLoading);
        SetPositionTriggerZone(_gridData.LevelTransitionPosition, _triggerZoneLevelTransition);
    }

    private void SetPositionTriggerZone(IReadOnlyList<Vector3> zonePositions, PoolMono<TriggerZoneEnter> triggerZones)
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
            Debug.Log("СписокПуст");
        return;
    }
}
