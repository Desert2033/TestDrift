using System.Collections.Generic;
using UnityEngine;

public class RegisterProgressReadersService : IRegisterProgressReadersService
{
    public List<ISavedProgressReader> _readers = new List<ISavedProgressReader>();
    public List<ISavedProgress> _writers = new List<ISavedProgress>();

    private IPersistentProgressService _progressService;

    public RegisterProgressReadersService(IPersistentProgressService persistentProgressService)
    {
        _progressService = persistentProgressService;
    }

    public void RegisterReader(ISavedProgressReader reader) =>
        _readers.Add(reader);

    public void RegisterWriter(ISavedProgress writer) =>
        _writers.Add(writer);

    public void NotifyReaders()
    {
        foreach (ISavedProgressReader reader in _readers)
        {
            reader.LoadProgess(_progressService.Progress);
        }
    }

    public void NotifyWriters()
    {
        foreach (ISavedProgress writer in _writers)
        {
            writer.UpdateProgress(_progressService.Progress);
        }
    }

    public void CleanUp()
    {
        _readers.Clear();
        _writers.Clear();
    }
}
