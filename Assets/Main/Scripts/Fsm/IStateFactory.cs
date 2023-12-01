using System;

namespace Assets.Main.Scripts.Fsm
{
    public interface IStateFactory : IDisposable
    {
        T Create<T>(IStateMachine stateMachine) where T : IState;
    }
}