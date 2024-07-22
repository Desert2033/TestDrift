public interface IRegisterProgressReadersService : IService
{
    void CleanUp();
    void NotifyReaders();
    void NotifyWriters();
    void RegisterReader(ISavedProgressReader reader);
    void RegisterWriter(ISavedProgress writer);
}