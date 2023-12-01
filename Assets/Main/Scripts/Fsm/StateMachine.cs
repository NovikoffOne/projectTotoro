using System;

namespace Assets.Main.Scripts.Fsm
{
    public class StateMachine : IStateMachine
    {
        public IState CurrentState { get; private set; }

        public void ChangeState<T>(Action<T> action = null) where T : IState, new()
        {
            if (CurrentState != null)
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
}