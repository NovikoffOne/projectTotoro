using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuthorizePanel : MonoBehaviour, IPanel
{
    [SerializeField] private Button _authorizeButton;
    [SerializeField] private Button _dontAuthorizeButton;

    private List<Button> _buttons = new List<Button>();

    public List<Button> Buttons => _buttons;

    public Button AuthorizeButton => _authorizeButton;
    public Button DontAuthorizeButton => _dontAuthorizeButton;

    private void Start()
    {
        _buttons.Add(_authorizeButton);
        _buttons.Add(_dontAuthorizeButton);
    }
}
