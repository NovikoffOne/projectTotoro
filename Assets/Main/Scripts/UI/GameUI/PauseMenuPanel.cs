﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

internal class PauseMenuPanel : MonoBehaviour, IPanel
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _reloadButton;

    private List<Button> _buttons;

    public Button PlayButton => _playButton;
    public Button CloseButton => _closeButton;
    public Button ReloadButton => _reloadButton;

    public List<Button> Buttons => _buttons;

    private void Start()
    {
        _buttons.Add(_reloadButton);
        _buttons.Add(_closeButton);
        _buttons.Add(_playButton);
    }
}
