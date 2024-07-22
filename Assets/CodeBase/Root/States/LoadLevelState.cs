using Fusion;
using UnityEngine;

public class LoadLevelState : IState
{
    private IStateMachine _gameStateMachine;
    private INetworkProvider _networkProvider;

    public LoadLevelState(IStateMachine gameStateMachine, INetworkProvider networkProvider)
    {
        _gameStateMachine = gameStateMachine;
        _networkProvider = networkProvider;
    }

    public void Enter()
    {
        _networkProvider.OnGameStarted += OnGameStarted;
    }

    private void OnGameStarted(NetworkRunner runner)
    {
        _gameStateMachine.Enter<GameLoopState>();
    }

    public void Exit()
    {
    }
}
