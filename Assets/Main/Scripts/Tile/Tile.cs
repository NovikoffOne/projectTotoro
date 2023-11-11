using UnityEngine;

public class Tile : MonoBehaviour,
    IEventReceiver<PlayerCanInputed>
{
    [SerializeField] private GameObject _baseTile;
    [SerializeField] private GameObject _offsetTile;
    [SerializeField] private GameObject _highlightingTile;

    private GameObject _currentTile;

    private bool _canInput;

    public Vector3 Position => transform.position;

    private void OnDestroy()
    {
        this.Unsubscribe<PlayerCanInputed>();
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

        this.Subscribe<PlayerCanInputed>();
    }

    public void OnEvent(PlayerCanInputed var)
    {
        _canInput = var.IsCanInput;
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
