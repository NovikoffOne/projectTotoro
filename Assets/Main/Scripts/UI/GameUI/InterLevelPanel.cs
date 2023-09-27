﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

internal class InterLevelPanel : MonoBehaviour, IPanel
{
    [SerializeField] private Button _newLevelButton;
    [SerializeField] private Button _exitMenuButton;
    [SerializeField] private Button _reloadButton;

    private List<Button> _buttons = new List<Button>();
    public Button NewLevelButton => _newLevelButton;
    public Button ExitMenuButton => _exitMenuButton;
    public Button ReloadButton => _reloadButton;

    public List<Button> Buttons => _buttons;

    private void Start()
    {
        _buttons.Add(_newLevelButton);
        _buttons.Add(_exitMenuButton);
        _buttons.Add(_reloadButton);
    }
}
