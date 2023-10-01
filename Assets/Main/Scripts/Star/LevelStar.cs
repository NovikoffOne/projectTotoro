using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Threading;
using UnityEngine;

public class LevelStar : MonoBehaviour,
    IEventReceiver<ClickGameActionEvent>,
    IEventReceiver<StartGame>,
    IEventReceiver<NewGame>,
    IEventReceiver<PlayerCanInput>
{
    private int _count;
    private int _levelIndex;

    private float _allTime = 60;
    private float _time = 0;

    private bool _timerOn = false;
    public float Count => _count;

    private void Start()
    {
        this.Subscribe<ClickGameActionEvent>();
        this.Subscribe<StartGame>();
        this.Subscribe<PlayerCanInput>();
    }

    private void Update()
    {
        if (_timerOn)
            Timer();
    }

    public void OnEvent(ClickGameActionEvent var)
    {
        if(var.GameAction == GameAction.Completed)
        {
            _timerOn = false;

            _count = CalculateStar();

            var data = new PlayerStarData()
            {
                Count = _count,
                LevelIndex = _levelIndex
            };

            //RepositoryHelper.Save(data, new ServerReposytory(), $"Level {_levelIndex}");
            RepositoryHelper.Save(data, new LocalReposytory(), $"Level {_levelIndex}");

            _time = 0;
        }
    }

    public void OnEvent(StartGame var)
    {
        _timerOn = true;
    }

    public void Timer()
    {
        _time += Time.deltaTime;
    }

    public void OnEvent(PlayerCanInput var)
    {
        _timerOn = var.IsCanInput;
    }

    private int CalculateStar()
    {
        if (_time > _allTime)
            return 0;
        else if (_time >= 50)
            return 1;
        else if (_time >= 40)
            return 2;
        else if (_time < 40)
            return 3;

        return 0;
    }

    public void OnEvent(NewGame var)
    {
        _levelIndex = var.IndexLevel;
        Debug.Log(var.IndexLevel);
    }
}
