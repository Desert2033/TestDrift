using System;
using System.Collections.Generic;

public class StateMachine : IStateMachine
{
    private Dictionary<Type, IExitableState> _states;
    private IExitableState _currentState;

    /*public StateMachine(IPersistentProgressService persistentProgressService,
        ISaveLoadService saveLoadService,
        ISceneLoader sceneLoader)
    {
        _states = new Dictionary<Type, IExitableState>()
        {
            [typeof(GameBootstrapState)] = new GameBootstrapState(this),
            [typeof(LoadDataState)] = new LoadDataState(this, saveLoadService),
            [typeof(LoadMenuState)] = new LoadMenuState(this, sceneLoader),
            [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader),
            [typeof(GameLoopState)] = new GameLoopState(this),
        };
    }*/

    public void InitStates(Dictionary<Type, IExitableState> states)
    {
        _states = states;
    }

    public void Enter<TState>() where TState : class, IState
    {
        IState state = ChangeState<TState>();
        state.Enter();
    }

    public void Enter<TState, TParameter>(TParameter parameter) where TState : class, IStateWithParameter<TParameter>
    {
        TState state = ChangeState<TState>();
        state.Enter(parameter);
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
        _currentState?.Exit();

        TState state = GetState<TState>();
        _currentState = state;

        return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState =>
        _states[typeof(TState)] as TState;
}
