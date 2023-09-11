using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject _baseTile;
    [SerializeField] private GameObject _offsetTile;
    [SerializeField] private GameObject _highlightingTile;

    private GameObject _currentTile;
    
    public Vector3 Position => transform.position;

    public void Init(bool isOffset)
    {
        if (isOffset)
        {
            _offsetTile.SetActive(true);
            _currentTile = _offsetTile;
        }

        else
        {
            _baseTile.SetActive(true);
            _currentTile = _baseTile;
        }
    }

    public void OnMouseEnter()
    {
        Highlight(_highlightingTile);
    }

    public void OnMouseExit()
    {
        RevertBase(_highlightingTile);
    }

    private void RevertBase(GameObject tile)
    {
        tile.SetActive(false);
        _currentTile.SetActive(true);
    }

    private void Highlight(GameObject tile)
    {
        tile.SetActive(true);
        _currentTile.SetActive(false);
    }
}
