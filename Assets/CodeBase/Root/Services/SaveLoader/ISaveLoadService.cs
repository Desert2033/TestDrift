public interface ISaveLoadService : IService
{
    PlayerProgress LoadProgress();
    void SavedProgress();
}