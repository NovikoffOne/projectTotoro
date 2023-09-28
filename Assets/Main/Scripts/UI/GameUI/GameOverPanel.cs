using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour, IPanel
{
    [SerializeField] private Button _exitMenuButton;
    [SerializeField] private Button _reloadButton;

    private List<Button> _buttons = new List<Button>();

    public Button ExitMenuButton => _exitMenuButton;
    public Button ReloadButton => _reloadButton;

    public List<Button> Buttons => _buttons;

    private void Start()
    {
        _buttons.Add(_reloadButton);
        _buttons.Add(_exitMenuButton);
    }
}
