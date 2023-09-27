using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(MapManagerData), menuName = "Data/Create new MapManagerData", order = 51)]

public class MapManagerData : ScriptableObject
{
    [SerializeField] private List<GridData> _gridData;
    [SerializeField] private GridGenerator _gridPrefab;

    [SerializeField] private int _minNumberPassengersCarried;

    public IReadOnlyList<GridData> GridData => _gridData;

    public int MinNumberPassengersCarried => _minNumberPassengersCarried;

    public GridGenerator GridGenerator => _gridPrefab;
}
