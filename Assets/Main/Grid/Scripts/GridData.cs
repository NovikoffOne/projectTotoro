using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(GridData), menuName = "Data/Create new GridData", order = 51)]

public class GridData : ScriptableObject
{
    [SerializeField] private List<TriggerZone> _zonePrefabs;

    [SerializeField] private List<Vector3> _acceleratorPosition;
    [SerializeField] private List<Vector3> _barriersPosition;
    [SerializeField] private List<Vector3> _repulsorPosition;
    [SerializeField] private List<Vector3> _landingPlacePosition;
    [SerializeField] private List<Vector3> _loadingPlacePosition;
    [SerializeField] private List<Vector3> _levelTransitionPosition;

    [SerializeField] private int _width;
    [SerializeField] private int _height;

    public IReadOnlyList<Vector3> BarriersPosition => _barriersPosition;
    public IReadOnlyList<Vector3> RepulsorPosition => _repulsorPosition;
    public IReadOnlyList<Vector3> AcceleratorPosition => _acceleratorPosition;
    public IReadOnlyList<Vector3> LandingPlacePosition => _landingPlacePosition;
    public IReadOnlyList<Vector3> LoadingPlacePosition => _loadingPlacePosition;
    public IReadOnlyList<Vector3> LevelTransitionPosition => _levelTransitionPosition;

    public int Width => _width;
    public int Height => _height;

    public TriggerZone GetTriggerZone(ZoneIndex zone)
    {
        var triggerZone = _zonePrefabs.Find(element => element.ZoneIndex == zone);

        if (triggerZone == null)
            throw new System.NullReferenceException("список пуст");

        return triggerZone;
    }
}
