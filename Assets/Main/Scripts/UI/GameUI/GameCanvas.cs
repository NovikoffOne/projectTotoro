using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

internal class GameCanvas : MonoBehaviour, IView
{
    [SerializeField] private Button _pauseButton;

    [SerializeField] private InterLevelPanel _interLevelPanel;
    [SerializeField] private PauseMenuPanel _pauseMenuPanel;
    [SerializeField] private GameOverPanel _gameOverPanel;

    private List<IPanel> _panels;

    public IReadOnlyList<IPanel> Panels => _panels;

    public Button PauseButton => _pauseButton;

    public InterLevelPanel InterLevelPanel => _interLevelPanel;
    public PauseMenuPanel PauseMenuPanel => _pauseMenuPanel;
    public GameOverPanel GameOverPanel => _gameOverPanel;

    private void Start()
    {
        this.AddController<GameCanvasController>();
        _panels.Add(_interLevelPanel);
    }
}