using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class AuthorizePanel : MonoBehaviour, IPanel
{
    [SerializeField] private Button _authorizeButton;
    [SerializeField] private Button _dontAuthorizeButton;

    private List<Button> _buttons = new List<Button>(); 

    public Button AuthorizeButton => _authorizeButton;
    public Button DontAuthorizeButton => _dontAuthorizeButton;

    public List<Button> Buttons => _buttons;

    private void Start()
    {
        _buttons.Add(_authorizeButton);
        _buttons.Add(_dontAuthorizeButton);
    }
}
