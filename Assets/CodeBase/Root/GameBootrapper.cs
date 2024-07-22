using System;
using UnityEngine;
using Zenject;
using System.Collections.Generic;

public class GameBootrapper : MonoBehaviour
{
    private IStateMachine _gameStateMachine;
    private ISaveLoadService _saveLoadService;
    private SceneLoader _sceneLoader;
    private IStaticDataService _staticDataService;
    private INetworkProvider _networkProvider;
    private INetworkFactory _networkFactory;
    private ICarFactory _carFactory;
    private IRegisterProgressReadersService _registerReadersService;
    private IPersistentProgressService _progressService;
    private IIAPService _iapService;
    private ICoroutineRunner _coroutineRunner;
    private ITimeOverService _timeOverService;
    private IDriftScoreService _driftScoreService;
    private IAdsService _adsService;
    private ICarSpawnPositionService _carSpawnPositionService;
    private IWalletService _walletService;

    [Inject]
    public void Construct(IStateMachine gameStateMachine,
        ISaveLoadService saveLoadService,
        IStaticDataService staticDataService,
        INetworkProvider networkProvider,
        INetworkFactory networkFactory,
        ICarFactory carFactory,
        IRegisterProgressReadersService registerReadersService,
        IPersistentProgressService progressService,
        IIAPService iapService,
        ICoroutineRunner coroutineRunner,
        ITimeOverService timeOverService,
        IDriftScoreService driftScoreService,
        IAdsService adsService,
        ICarSpawnPositionService carSpawnPositionService,
        IWalletService walletService,
        SceneLoader sceneLoader)
    {
        _gameStateMachine = gameStateMachine;
        _saveLoadService = saveLoadService;
        _sceneLoader = sceneLoader;
        _staticDataService = staticDataService;
        _networkProvider = networkProvider;
        _networkFactory = networkFactory;
        _carFactory = carFactory;
        _registerReadersService = registerReadersService;
        _progressService = progressService;
        _iapService = iapService;
        _coroutineRunner = coroutineRunner;
        _timeOverService = timeOverService;
        _driftScoreService = driftScoreService;
        _adsService = adsService;
        _carSpawnPositionService = carSpawnPositionService;
        _walletService = walletService;
    }

    private void Start()
    {
        InitStates();

        DontDestroyOnLoad(this);
    }

    private void InitStates()
    {
        _gameStateMachine.InitStates(new Dictionary<Type, IExitableState>()
        {
            [typeof(GameBootstrapState)] = new GameBootstrapState(_gameStateMachine, _staticDataService, _iapService, _adsService),
            [typeof(LoadProgressState)] = new LoadProgressState(_gameStateMachine, _saveLoadService, _progressService),
            [typeof(LoadMainMenuState)] = new LoadMainMenuState(_gameStateMachine,
            _sceneLoader,
            _staticDataService,
            _networkFactory,
            _networkProvider),
            [typeof(LoadCarShopState)] = new LoadCarShopState(_gameStateMachine, _sceneLoader, _carFactory, _registerReadersService),
            [typeof(LoadLevelState)] = new LoadLevelState(_gameStateMachine, _networkProvider),
            [typeof(GameLoopState)] = new GameLoopState(
                _gameStateMachine, 
                _networkProvider, 
                _coroutineRunner, 
                _timeOverService,
                _driftScoreService,
                _carSpawnPositionService,
                _walletService,
                _saveLoadService
                ),
        });

        _gameStateMachine.Enter<GameBootstrapState>();
    }
}
