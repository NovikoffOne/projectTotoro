using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Tile : MonoBehaviour,
    IEventReceiver<OpenPauseMenu>
{
    [SerializeField] private GameObject _baseTile;
    [SerializeField] private GameObject _offsetTile;
    [SerializeField] private GameObject _highlightingTile;

    private GameObject _currentTile;

    private bool _canInput;

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

        this.Subscribe<OpenPauseMenu>();
    }

    public void OnMouseEnter()
    {
        if (Time.timeScale == 0 || !_canInput)
            return;

        Highlight(_highlightingTile);
    }

    public void OnMouseExit()
    {
        RevertBase(_highlightingTile);
    }

    public void OnEvent(OpenPauseMenu var)
    {
        _canInput = var.CanInput;
    }

    private void OnDestroy()
    {
        this.Unsubscribe<OpenPauseMenu>();
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
