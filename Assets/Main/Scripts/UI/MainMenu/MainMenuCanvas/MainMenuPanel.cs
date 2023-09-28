using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour, IPanel
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _liderBoardButton;
    [SerializeField] private Button _settingsButton;

    private List<Button> _buttons = new List<Button>();

    public Button PlayButton => _playButton;
    public Button LiderBoardButton => _liderBoardButton;
    public Button SettingsButton => _settingsButton;

    public List<Button> Buttons => _buttons;

    private void Start()
    {
        _buttons.Add(_playButton);
        _buttons.Add(_liderBoardButton);
        _buttons.Add(_settingsButton);
    }
}
