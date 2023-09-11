using System;

namespace Assets.Main.Scripts.Test
{
    public interface IStateMachine : IDisposable
    {
        IState CurrentState { get; }

        void ChangeState(IState state);

        event Action OnUpdate;
    }

    public interface IState : IDisposable
    {
        void Enter();
        void Update();
        void Exit();
    }

    public interface IStateFactory : IDisposable
    {
        T Create<T>(IStateMachine stateMachine) where T : IState;
    }

    public abstract class BaseState<T> : IState
        where T : IStateMachine
    {
        private readonly T Machine;

        public BaseState(T machine)
        {
            Machine = machine;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }

        protected virtual void OnDispose() { }
    }
}
