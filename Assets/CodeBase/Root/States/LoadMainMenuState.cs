using UnityEngine;

public class LoadMainMenuState : IState
{
    private IStateMachine _gameStateMachine;
    private ISceneLoader _sceneLoader;
    private IStaticDataService _apiDataService;
    private INetworkFactory _networkFactory;
    private INetworkProvider _networkProvider;

    public LoadMainMenuState(IStateMachine gameStateMachine, 
        ISceneLoader sceneLoader, 
        IStaticDataService apiDataService,
        INetworkFactory networkFactory,
        INetworkProvider networkProvider)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _apiDataService = apiDataService;
        _networkFactory = networkFactory;
        _networkProvider = networkProvider;
    }

    public void Enter()
    {
        _sceneLoader.Load("MainMenuScene", OnLoaded);
    }

    private void OnLoaded()
    {
        _networkProvider.JoinLobby();
    }

    public void Exit()
    {
    }
}
