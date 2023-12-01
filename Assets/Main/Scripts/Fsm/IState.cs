using System;

namespace Assets.Main.Scripts.Fsm
{
    public interface IState : IDisposable
    {
        public IStateMachine StateMachine { get; set; }

        void Enter();
        void Update();
        void Exit();
    }
}