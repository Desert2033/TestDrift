using System;
using System.Collections;
using UnityEngine;

public class GameLoopState : IState
{
    private const float TimeSecondsOfLevel = 120f;

    private IStateMachine _stateMachine;
    private INetworkProvider _networkProvider;
    private ICoroutineRunner _coroutineRunner;
    private ITimeOverService _timeOverService;
    private IDriftScoreService _driftScoreService;
    private ICarSpawnPositionService _carSpawnPositionService;
    private IWalletService _walletService;
    private ISaveLoadService _saveLoadService;

    public GameLoopState(IStateMachine stateMachine, 
        INetworkProvider networkProvider, 
        ICoroutineRunner coroutineRunner,
        ITimeOverService timeOverService,
        IDriftScoreService driftScoreService,
        ICarSpawnPositionService carSpawnPositionService,
        IWalletService walletService,
        ISaveLoadService saveLoadService)
    {
        _stateMachine = stateMachine;
        _networkProvider = networkProvider;
        _coroutineRunner = coroutineRunner;
        _timeOverService = timeOverService;
        _driftScoreService = driftScoreService;
        _carSpawnPositionService = carSpawnPositionService;
        _walletService = walletService;
        _saveLoadService = saveLoadService;
    }
    public void Enter()
    {
        if (_networkProvider.IsServer())
        {
            StartLevelTime();
        }
    }

    public void Exit()
    {
        _walletService.RaiseCash(_driftScoreService.WinScore);

        _driftScoreService.SetTotalScoreZero();
        _driftScoreService.SetWinScore(0);
        _carSpawnPositionService.Clear();
        _saveLoadService.SavedProgress();
    }

    private void StartLevelTime()
    {
        _coroutineRunner.StartCoroutine(LevelTimeCoroutine(TimeSecondsOfLevel));
    }

    private IEnumerator LevelTimeCoroutine(float time)
    {
        _timeOverService.SetTimeOver(time);

        while (_timeOverService.TimeOver >= 0f)
        {
            yield return new WaitForSeconds(1f);
            _timeOverService.RemoveTime(1f);
        }

        _networkProvider.Shutdown();
    }
}
