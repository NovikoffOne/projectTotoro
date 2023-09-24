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


    public Button PauseButton => _pauseButton;

    public InterLevelPanel InterLevelPanel => _interLevelPanel;
    public PauseMenuPanel PauseMenuPanel => _pauseMenuPanel;
    public GameOverPanel GameOverPanel => _gameOverPanel;


}