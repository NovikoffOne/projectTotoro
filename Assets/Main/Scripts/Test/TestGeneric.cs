using DG.Tweening;
using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Main.Scripts.Test
{
    public interface IStateMachine : IDisposable
    {
        IState CurrentState { get; }

        void ChangeState<T>(Action<T> action = null) where T : IState, new();

        void Update();
    }

    public interface IState : IDisposable
    {
        public IStateMachine StateMachine { get; set; }

        void Enter();
        void Update();
        void Exit();
    }

    public interface IStateFactory : IDisposable
    {
        T Create<T>(IStateMachine stateMachine) where T : IState;
    }

    public abstract class BaseState<T> : IState
       where T : class
    {
        private T _target;

        private IStateMachine _stateMachine;

        public BaseState()
        {
            
        }

        public T Target 
        {
            get => _target;

            set
            {
                if (_target == null)
                    _target = value;
            }
        }

        public IStateMachine StateMachine
        {
            get => _stateMachine;

            set
            {
                if (_stateMachine == null)
                    _stateMachine = value;
            }
        }

        public void Dispose()
        {
            OnDispose();
            GC.SuppressFinalize(this);
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }

        protected virtual void OnDispose() { }
    }

    public class StateMachine : IStateMachine
    {
        public IState CurrentState { get; private set; }

        public void ChangeState<T>(Action<T> action = null) where T : IState, new()
        {
            if(CurrentState != null)
            {
                if (typeof(T).Equals(CurrentState.GetType()))
                    return;

                CurrentState.Exit();
                CurrentState.Dispose();
            }

            var state = Activator.CreateInstance<T>();

            state.StateMachine = this;

            action?.Invoke(state);

            CurrentState = state;

            CurrentState.Enter();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void Update()
        {
            CurrentState?.Update();
        }
    }

    public class MoveStateAnimation : BaseState<PlayerView>
    {
        public override void Enter()
        {
            Target.transform.Rotate(Vector3.up, 360);
        }

        public override void Exit() 
        {
            Target.transform.Rotate(Vector3.up, 360);
        }

        public override void Update()
        {

        }
    }

    public class MoveUpPlayerView : BaseState<PlayerView>
    {
        public override void Enter()
        {
            Target.transform.Translate(Vector3.up * 3, Space.World);
        }

        public override void Exit()
        {
            Target.transform.Translate(Vector3.up * 3, Space.World);
        }

        public override void Update()
        {

        }
    }
}
