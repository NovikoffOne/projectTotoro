using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour, IView
{
    [SerializeField] private Button _pauseButton;

    [SerializeField] private InterLevelPanel _interLevelPanel;
    [SerializeField] private PauseMenuPanel _pauseMenuPanel;
    [SerializeField] private GameOverPanel _gameOverPanel;

    private List<IPanel> _panels = new List<IPanel>();

    public IReadOnlyList<IPanel> Panels => _panels;

    public Button PauseButton => _pauseButton;

    public InterLevelPanel InterLevelPanel => _interLevelPanel;
    public PauseMenuPanel PauseMenuPanel => _pauseMenuPanel;
    public GameOverPanel GameOverPanel => _gameOverPanel;

    public event Action OnDestroyded;

    private void Start()
    {
        this.AddController<GameCanvasController>();

        _panels.Add(_interLevelPanel);
        _panels.Add(_pauseMenuPanel);
        _panels.Add(_gameOverPanel);
    }

    private void OnDestroy()
    {
        OnDestroyded?.Invoke();
    }
}