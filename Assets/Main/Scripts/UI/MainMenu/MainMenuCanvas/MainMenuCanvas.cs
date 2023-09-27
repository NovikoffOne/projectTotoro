using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCanvas : MonoBehaviour, IView
{
    [SerializeField] private MainMenuPanel _menuPanel;

    public MainMenuPanel MenuPanel => _menuPanel;

    private void Start()
    {
        this.AddController<MainMenuController>();
    }
}
