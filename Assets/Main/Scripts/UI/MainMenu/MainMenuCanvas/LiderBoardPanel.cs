using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiderBoardPanel : MonoBehaviour,
    IPanel
{
    [SerializeField] private Button _close;

    [SerializeField] private PlayerStringLiderboard _currentPlayerScore;

    [SerializeField] private List<PlayerStringLiderboard> _playerStrings = new List<PlayerStringLiderboard>();

    private List<Button> _buttons = new List<Button>();

    public Button Close => _close;
    public List<Button> Buttons => _buttons;
    public PlayerStringLiderboard CurrentPlayerScore => _currentPlayerScore;
    public List<PlayerStringLiderboard> PlayersStrings => _playerStrings;


    private void Start()
    {
        _buttons.Add(_close);

        this.gameObject.SetActive(false);
    }
}

