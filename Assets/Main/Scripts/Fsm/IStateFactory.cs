using System;

public interface IStateFactory : IDisposable
{
    T Create<T>(IStateMachine stateMachine) where T : IState;
}
