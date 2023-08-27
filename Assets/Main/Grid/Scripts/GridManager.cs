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

    [SerializeField] private Transform _tilePool;
    [SerializeField] private Transform _triggerZonePool;

    private PoolMono<Tile> _pool;
   
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

                var tempTile = _pool.GetFreeTile();

                tempTile.transform.position = new Vector3(x, y, transform.position.z);

                tempTile.Init(isOffset);

                tempTile.name = $"Tile {x} {y}";

                tempTile.gameObject.SetActive(true);
            }
        }
    }

    private void GeneraneGrid()
    {
        _pool = new PoolMono<Tile>(_tilePrefab, _gridData.Width * _gridData.Height, _tilePool);

        InstantiateGrid();

        _pool.ArrangeTriggerZone(_gridData.BarriersPosition, _gridData.GetTriggerZone(ZoneIndex.Barier), _triggerZonePool);
        _pool.ArrangeTriggerZone(_gridData.AcceleratorPosition, _gridData.GetTriggerZone(ZoneIndex.Accelerartor), _triggerZonePool);
        _pool.ArrangeTriggerZone(_gridData.RepulsorPosition, _gridData.GetTriggerZone(ZoneIndex.Repulsor), _triggerZonePool);
        _pool.ArrangeTriggerZone(_gridData.LandingPlacePosition, _gridData.GetTriggerZone(ZoneIndex.Landing), _triggerZonePool);
        _pool.ArrangeTriggerZone(_gridData.LoadingPlacePosition, _gridData.GetTriggerZone(ZoneIndex.Loading), _triggerZonePool);
        _pool.ArrangeTriggerZone(_gridData.LevelTransitionPosition, _gridData.GetTriggerZone(ZoneIndex.LevelTransition), _triggerZonePool);
    }
}
