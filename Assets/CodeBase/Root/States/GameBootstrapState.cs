public class GameBootstrapState : IState
{
    private IStateMachine _gameStateMachine;
    private IStaticDataService _staticDataService;
    private IIAPService _iapService;
    private IAdsService _adsService;

    public GameBootstrapState(IStateMachine gameStateMachine, 
        IStaticDataService staticDataService,
        IIAPService iapService,
        IAdsService adsService)
    {
        _gameStateMachine = gameStateMachine;
        _staticDataService = staticDataService;
        _iapService = iapService;
        _adsService = adsService;
    }

    public void Enter()
    {
        _staticDataService.LoadCarsData();
        _staticDataService.LoadRoomData();

        _iapService.Initialize();
        _adsService.Init();

        _gameStateMachine.Enter<LoadProgressState>();
    }

    public void Exit()
    {
    }
}
