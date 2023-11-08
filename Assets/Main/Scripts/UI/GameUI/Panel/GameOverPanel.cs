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
    [SerializeField] private Button _rewardButton;

    private List<Button> _buttons = new List<Button>();

    public List<Button> Buttons => _buttons;

    public Button ExitMenuButton => _exitMenuButton;
    public Button ReloadButton => _reloadButton;
    public Button RewardButton => _rewardButton;

    private void Start()
    {
        _buttons.Add(_reloadButton);
        _buttons.Add(_exitMenuButton);
        _buttons.Add(_rewardButton);
    }
}
