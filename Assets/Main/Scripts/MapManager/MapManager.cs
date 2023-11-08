using LayerLab;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using IJunior.TypedScenes;
using System.Dynamic;
using System.Xml.Schema;

public class MapManager :
    IEventReceiver<EnergyChangeEvent>,
    IEventReceiver<OnPlayerInsided>,
    IEventReceiver<ClickGameActionEvent>,
    IEventReceiver<NewGame>,
    //IEventReceiver<StartGame>,
    IEventReceiver<IsRewarded>
{
    private Player _player; 

    private int _numberPassengersCarried; 

    private int _gridIndex;

    private MapManagerData _mapManagerData;

    private PoolMono<Player> _playerPool;

    private GridGenerator _grid;

    public MapManager(MapManagerData mapManagerData, PoolMono<Player> poolPlayer)
    {
        _grid = new GridGenerator();

        _mapManagerData = mapManagerData;
        _playerPool = poolPlayer;

        SubscribeAll();
    }

    private bool IsCanTransition => _numberPassengersCarried >= _mapManagerData.MinNumberPassengersCarried;
    public int GridIndex => _gridIndex;

    public void NewLevel(int index=0)
    {
        if(_player == null || _player.gameObject.activeSelf == false)
            _player = _playerPool.Spawn();

        _numberPassengersCarried = 0;

        _gridIndex = index;
        
        if (_mapManagerData.GridData.Count > _gridIndex)
        {
            _grid.NewGrid(_mapManagerData.GridData[_gridIndex]);
        }
        else 
        {
            DespawnPlayer();
            IJunior.TypedScenes.MainMenu.Load(); // Заглушка
        }
    }

    public void DespawnPlayer()
    {
        _player.Movement.ResetPosition();
        _playerPool?.DeSpawn(_player);
    }

    public void OnEvent(IsRewarded var)
    {
        EventBus.Raise(new PlayerCanInput(true));
    }

    public void OnEvent(EnergyChangeEvent var)
    {
        if (!var.IsChargeChange && _gridIndex == 0)
        {
            EventBus.Raise(new ChangeTutorialState(3));
        }

        if (var.IsChargeChange)
        {
            _numberPassengersCarried++;
        }

        if (IsCanTransition)
        {
            EventBus.Raise(new OpenLevelTransition());

            if (_gridIndex == 0)
                EventBus.Raise(new ChangeTutorialState(5));
        }
    }

    public void OnEvent(NewGame var)
    {
        NewLevel(var.IndexLevel);
        EventBus.Raise(new StartGame(_gridIndex));
    }

    public void OnEvent(ClickGameActionEvent var)
    {
        switch (var.GameAction)
        {
            case GameAction.ClickReload:
                DespawnPlayer();
                NewLevel(_gridIndex);
                break;

            case GameAction.ClickNextLevel:
                DespawnPlayer();
                NewLevel(++_gridIndex);
                break;

            case GameAction.GameOver:
                EventBus.Raise(new PlayerCanInput(false));
                break;

            case GameAction.Start:
                if (_gridIndex == 0)
                    EventBus.Raise(new ChangeTutorialState(0));
                else
                    EventBus.Raise(new ChangeTutorialState(0, false));
                break;

            case GameAction.Exit:
                DespawnPlayer();
                IJunior.TypedScenes.MainMenu.Load();
                break;

            default:
                break;
        }
    }

    public void OnEvent(OnPlayerInsided var)
    {
        if (IsCanTransition)
        {
            EventBus.Raise(new ClickGameActionEvent(GameAction.Completed));
            DespawnPlayer();
        }
        else
            Debug.Log("Нет питания");
    }

    private void SubscribeAll()
    {
        this.Subscribe<EnergyChangeEvent>();
        this.Subscribe<ClickGameActionEvent>();
        this.Subscribe<OnPlayerInsided>();
        this.Subscribe<NewGame>();
        this.Subscribe<IsRewarded>();
    }
}