using System;

public interface IAdsService : IService 
{
    public void Init();
    void ShowRewardedVideo(Action onVideoFinished);

    public bool IsInitialized { get; }
    public string RewardedId { get; }
}