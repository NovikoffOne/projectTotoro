using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;

    [SerializeField] private Tile _tilePrefab;

    [SerializeField] private List<TriggerZone> _zonePrefabs = new List<TriggerZone>();

    [SerializeField] private List<Vector3> _acceleratorPosition = new List<Vector3>();
    [SerializeField] private List<Vector3> _barriersPosition = new List<Vector3>();
    [SerializeField] private List<Vector3> _repulsorPosition = new List<Vector3>();
    [SerializeField] private List<Vector3> _landingPlacePosition = new List<Vector3>();
    [SerializeField] private List<Vector3> _loadingPlacePosition = new List<Vector3>();
    [SerializeField] private List<Vector3> _levelTransitionPosition = new List<Vector3>();

    [SerializeField] private Camera _camera;

    [SerializeField] private enum ZoneIndex
    {

    }

    public LevelTransition GetLevelTransition => GetComponentInChildren<LevelTransition>();
    public List<LandingPlace> GetLandingPlaces => GetComponentsInChildren<LandingPlace>().ToList();

    private void Awake()
    {
        GenerateGrid();

        ArrangeTriggerZone(_barriersPosition, _zonePrefabs[0]);

        ArrangeTriggerZone(_acceleratorPosition, _zonePrefabs[1]);

        ArrangeTriggerZone(_repulsorPosition, _zonePrefabs[2]);
        
        ArrangeTriggerZone(_landingPlacePosition, _zonePrefabs[3]);
        
        ArrangeTriggerZone(_loadingPlacePosition, _zonePrefabs[4]);

        ArrangeTriggerZone(_levelTransitionPosition, _zonePrefabs[5]);
    }

    public List<LandingPlace> GetLandingList()
    {
        var tempList = GetComponentsInChildren<LandingPlace>().ToList();
        return tempList;
    }

    private void GenerateGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity, transform);
                spawnedTile.name = $"Tile {x} {y}";

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);

                spawnedTile.Init(isOffset);
            }
        }

        _camera.transform.position = new Vector3(((float)_width / 2 - 0.5f), -4.5f, -10);
    }

    private void ArrangeTriggerZone(List<Vector3> zonePositions, TriggerZone triggerZone)
    {
        if (zonePositions != null && zonePositions.Count > 0)
        {
            for (int i = 0; i < zonePositions.Count; i++)
            {
                var spawnedBarrier = Instantiate(triggerZone, zonePositions[i], Quaternion.identity, transform);

                spawnedBarrier.name = $"Barrier {spawnedBarrier.transform.position.x} {spawnedBarrier.transform.position.y}";
            }
        }
        else
            return;
    }
}
