using UnityEngine;

public class LoadProgressState : IState
{
    private IStateMachine _stateMachine;
    private IPersistentProgressService _persistentProgressService;
    private ISaveLoadService _saveLoadService;

    public LoadProgressState(IStateMachine stateMachine, 
        ISaveLoadService saveLoadService, 
        IPersistentProgressService persistentProgressService)
    {
        _stateMachine = stateMachine;
        _persistentProgressService = persistentProgressService;
        _saveLoadService = saveLoadService;
    }

    public void Enter()
    {
        LoadProgressOrInitNew();

        _stateMachine.Enter<LoadMainMenuState>();
    }

    public void Exit()
    {
    }

    private void LoadProgressOrInitNew() =>
           _persistentProgressService.Progress = _saveLoadService.LoadProgress() ?? new PlayerProgress();
}
