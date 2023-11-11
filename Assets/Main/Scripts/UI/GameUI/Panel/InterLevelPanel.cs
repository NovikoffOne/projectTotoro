using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterLevelPanel : MonoBehaviour, IPanel
{
    [SerializeField] private PrizeFiller _starFiller;

    [SerializeField] private Button _newLevelButton;
    [SerializeField] private Button _exitMenuButton;
    [SerializeField] private Button _reloadButton;

    private List<Button> _buttons = new List<Button>();

    public List<Button> Buttons => _buttons;

    public Button NewLevelButton => _newLevelButton;
    public Button ExitMenuButton => _exitMenuButton;
    public Button ReloadButton => _reloadButton;
    public PrizeFiller StarFiller => _starFiller;

    private void Start()
    {
        _buttons.Add(_newLevelButton);
        _buttons.Add(_exitMenuButton);
        _buttons.Add(_reloadButton);
    }
}
