using System;

namespace Assets.Main.Scripts.Fsm
{
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
}