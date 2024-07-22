using System;
using System.Collections.Generic;

public interface IStateMachine
{
    void InitStates(Dictionary<Type, IExitableState> states);
    void Enter<TState>() where TState : class, IState;
    void Enter<TState, TParameter>(TParameter parameter) where TState : class, IStateWithParameter<TParameter>;
}