using LayerLab;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using IJunior.TypedScenes;
using System.Dynamic;

public class MapManager :
    IEventReceiver<EnergyChangeEvent>,
    IEventReceiver<ClickGameActionEvent>
{
    private Player _player; // Вынести в гриддату

    private int _numberPassengersCarried = 0; // Вынести в гриддату

    private int _gridIndex = 0; // 

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

    public void NewLevel(int index=0)
    {
        _player = _playerPool.Spawn();

        _numberPassengersCarried = 0;

        _gridIndex += index;

        if (_mapManagerData.GridData.Count > _gridIndex)
            _grid.NewGrid(_mapManagerData.GridData[_gridIndex]);
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

    public void OnEvent(EnergyChangeEvent var)
    {
        if (var.IsChargeChange)
            _numberPassengersCarried++;

        if (IsCanTransition)
            Debug.Log("Ворота открыты");
    }

    private void SubscribeAll()
    {
        this.Subscribe<EnergyChangeEvent>();
        this.Subscribe<ClickGameActionEvent>();
    }

    public void OnEvent(ClickGameActionEvent var)
    {
        switch (var.GameAction)
        {
            case GameAction.ClickPlay:

                break;

            case GameAction.ClickReload:
                DespawnPlayer();
                NewLevel(0);
                break;

            case GameAction.ClickNextLevel:
                DespawnPlayer();
                NewLevel(1);
                break;

            case GameAction.Completed:
                if (IsCanTransition)
                {
                    DespawnPlayer();
                    //OpenMenu(_interLevelMenu);
                }
                else
                    Debug.Log("Нет питания");
                break;

            case GameAction.GameOver:
                DespawnPlayer();
                break;

            case GameAction.Exit:
                IJunior.TypedScenes.MainMenu.Load();
                break;

            case GameAction.Start:
                break;

            default:
                break;
        }
    }
}