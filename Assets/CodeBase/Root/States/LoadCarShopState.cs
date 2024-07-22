using UnityEngine;

public class LoadCarShopState : IState
{
    private const string SceneName = "CarShopScene";

    private ISceneLoader _sceneLoader;
    private IStateMachine _stateMachine;
    private ICarFactory _carFactory;
    private IRegisterProgressReadersService _registerReadersService;

    public LoadCarShopState(IStateMachine stateMachine, 
        ISceneLoader sceneLoader, 
        ICarFactory carFactory, 
        IRegisterProgressReadersService registerReadersService)
    {
        _sceneLoader = sceneLoader;
        _stateMachine = stateMachine;
        _carFactory = carFactory;
        _registerReadersService = registerReadersService;
    }

    public void Enter()
    {
        _sceneLoader.Load(SceneName, OnLoaded);
    }

    public void Exit()
    {
    }

    private void OnLoaded()
    {
        _registerReadersService.NotifyReaders();
    }
}
