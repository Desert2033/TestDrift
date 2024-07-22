using UnityEngine;

public class SaveLoadService : ISaveLoadService
{
    private const string ProgressKey = "Progress";

    private IPersistentProgressService _progressService;
    private IRegisterProgressReadersService _registerReadersService;

    public SaveLoadService(IPersistentProgressService progressService, IRegisterProgressReadersService registerReadersService)
    {
        _progressService = progressService;
        _registerReadersService = registerReadersService;
    }

    public PlayerProgress LoadProgress() =>
        PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();

    public void SavedProgress()
    {
        _registerReadersService.NotifyWriters();

        PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
    }
}
