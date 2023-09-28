using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionPanel : MonoBehaviour, IPanel
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _levelButton;

    private List<Button> _buttons = new List<Button>();
    public Button CloseButton => _closeButton;
    public Button LevelButton => _levelButton;
    public List<Button> Buttons => _buttons;

    private void Start()
    {
        _buttons.Add(_closeButton);
        _buttons.Add(_levelButton);
    }
}
