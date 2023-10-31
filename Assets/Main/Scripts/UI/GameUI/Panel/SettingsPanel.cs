using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour,
    IPanel
{
    [SerializeField] private Button _enButton;
    [SerializeField] private Button _ruButton;
    [SerializeField] private Button _tuButton;
    [SerializeField] private Button _close;

    private List<Button> _buttons = new List<Button>();

    public Button EnButton => _enButton;
    public Button RuButton => _ruButton;
    public Button TuButton => _tuButton;
    public Button Close => _close;

    public List<Button> Buttons => _buttons;

    private void Awake()
    {
        _buttons.Add(_enButton);
        _buttons.Add(_ruButton);
        _buttons.Add(_tuButton);
        _buttons.Add(_close);
    }
}


