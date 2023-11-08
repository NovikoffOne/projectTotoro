using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(MapManagerData), menuName = "Data/Create new MapManagerData", order = 51)]

public class MapManagerData : ScriptableObject
{
    [SerializeField] private List<GridData> _gridData;
    [SerializeField] private GridGenerator _gridPrefab;

    [SerializeField] private int _minNumberPassengersCarried;

    public IReadOnlyList<GridData> GridData => _gridData;

    public GridGenerator GridGenerator => _gridPrefab;

    public int MinNumberPassengersCarried => _minNumberPassengersCarried;
}
