using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiderBoardPanel : MonoBehaviour, IPanel
{
    [SerializeField] private Button _close;

    private List<Button> _buttons = new List<Button>();

    public Button Close => _close;

    public List<Button> Buttons => _buttons;

    private void Start()
    {
        _buttons.Add(_close);
    }
}

