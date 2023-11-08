using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCanvas : MonoBehaviour, IView
{
    [SerializeField] private MainMenuPanel _menuPanel;
    [SerializeField] private LiderBoardPanel _liderBoardPanel;
    [SerializeField] private LevelSelectionPanel _levelSelectionPanel;
    [SerializeField] private AuthorizePanel _authorizePanel;

    private List<IPanel> _panels = new List<IPanel>();

    public LiderBoardPanel LiderBoardPanel => _liderBoardPanel;
    public MainMenuPanel MenuPanel => _menuPanel;
    public LevelSelectionPanel LevelSelectionPanel => _levelSelectionPanel;
    public AuthorizePanel AuthorizePanel => _authorizePanel;

    public List<IPanel> Panels => _panels;

    private void Start()
    {
        this.AddController<MainMenuController>();

        _panels.Add(_menuPanel);
        _panels.Add(_levelSelectionPanel);
        _panels.Add(_liderBoardPanel);
        _panels.Add(_authorizePanel);
    }
}
