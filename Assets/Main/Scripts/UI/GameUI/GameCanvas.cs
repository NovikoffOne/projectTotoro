﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour, IView
{
    [SerializeField] private Button _pauseButton;

    [SerializeField] private InterLevelPanel _interLevelPanel;
    [SerializeField] private PauseMenuPanel _pauseMenuPanel;
    [SerializeField] private GameOverPanel _gameOverPanel;
    [SerializeField] private TutorialPanel _tutorialPanel;

    private List<IPanel> _panels = new List<IPanel>();

    public IReadOnlyList<IPanel> Panels => _panels;

    public Button PauseButton => _pauseButton;

    public InterLevelPanel InterLevelPanel => _interLevelPanel;
    public PauseMenuPanel PauseMenuPanel => _pauseMenuPanel;
    public GameOverPanel GameOverPanel => _gameOverPanel;
    public TutorialPanel TutorialPanel => _tutorialPanel;

    public event Action OnDestroyded;

    private void Start()
    {
        this.AddController<GameCanvasController>();

        _panels.Add(_interLevelPanel);
        _panels.Add(_pauseMenuPanel);
        _panels.Add(_gameOverPanel);
        _panels.Add(_tutorialPanel);
    }

    private void OnDestroy()
    {
        OnDestroyded?.Invoke();
    }
}