using Assets.Main.Scripts.Pool;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Main.Scripts.Generator
{
    public class LevelGenerator
    {
        private GridData _data;

        private PoolMono<Tile> _tilePool;

        private List<PoolMono<TriggerZoneEnter>> _trigerZonePools = new List<PoolMono<TriggerZoneEnter>>();

        private PoolMono<TriggerZoneEnter> _triggerZoneBarrier;
        private PoolMono<TriggerZoneEnter> _triggerZoneAccelerartor;
        private PoolMono<TriggerZoneEnter> _triggerZoneRepulsor;
        private PoolMono<TriggerZoneEnter> _triggerZoneLanding;
        private PoolMono<TriggerZoneEnter> _triggerZoneLoading;
        private PoolMono<TriggerZoneEnter> _triggerZoneLevelTransition;

        private int _tileFactory = 2;
        private int _widthFactory = 2;

        private float _cameraPositionFactory = 0.5f;

        private string _tileName = "Tile ";

        public void NewLevel(GridData gridData)
        {
            _data = gridData;

            if (_trigerZonePools.Count > 0)
                _trigerZonePools.ForEach(pool => pool.DeSpawnAll());
            else
            {
                _triggerZoneBarrier = new PoolMono<TriggerZoneEnter>(_data.GetTriggerZone(ZoneIndex.Barier));
                _triggerZoneAccelerartor = new PoolMono<TriggerZoneEnter>(_data.GetTriggerZone(ZoneIndex.Accelerartor));
                _triggerZoneRepulsor = new PoolMono<TriggerZoneEnter>(_data.GetTriggerZone(ZoneIndex.Repulsor));
                _triggerZoneLanding = new PoolMono<TriggerZoneEnter>(_data.GetTriggerZone(ZoneIndex.Landing));
                _triggerZoneLoading = new PoolMono<TriggerZoneEnter>(_data.GetTriggerZone(ZoneIndex.Loading));
                _triggerZoneLevelTransition = new PoolMono<TriggerZoneEnter>(_data.GetTriggerZone(ZoneIndex.LevelTransition));

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
                _tilePool = new PoolMono<Tile>(_data.TilePrefab);

            GenerateGrid();
        }

        public List<TriggerZoneEnter> GetLandingPlaces()
        {
            var landingPlaces = new List<TriggerZoneEnter>();

            foreach (var landing in _triggerZoneLanding.Pool)
                landingPlaces.Add(landing);

            return landingPlaces;
        }

        private void GenerateTile()
        {
            var countTile = 0;

            for (int x = 0; x < _data.Width; x++)
            {
                for (int y = 0; y < _data.Height; y++)
                {
                    var isOffset = countTile % _tileFactory == 0;

                    countTile++;

                    var tempTile = _tilePool.Spawn();

                    tempTile.transform.position = new Vector3(x, y, 0);

                    tempTile.Init(isOffset);

                    tempTile.name = $"{_tileName} {x} {y}";
                }
            }
        }

        private void GenerateGrid()
        {
            GenerateTile();

            SetPositionsTriggerZones();

            SetCameraPosition();
        }

        private void SetCameraPosition()
        {
            var centerPositionX = (float)_data.Width / _widthFactory - _cameraPositionFactory;

            Camera.main.transform.position = new Vector3(centerPositionX, _data.CameraPositionY, _data.CameraPositionZ);
        }

        private void SetPositionsTriggerZones()
        {
            SetPositionTriggerZone(_data.BarriersPosition, _triggerZoneBarrier);
            SetPositionTriggerZone(_data.AcceleratorPosition, _triggerZoneAccelerartor);
            SetPositionTriggerZone(_data.RepulsorPosition, _triggerZoneRepulsor);
            SetPositionTriggerZone(_data.LandingPlacePosition, _triggerZoneLanding);
            SetPositionTriggerZone(_data.LoadingPlacePosition, _triggerZoneLoading);
            SetPositionTriggerZone(_data.LevelTransitionPosition, _triggerZoneLevelTransition);
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

            return;
        }
    }
}