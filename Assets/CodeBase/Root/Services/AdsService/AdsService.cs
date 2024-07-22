using UnityEngine.Advertisements;
using UnityEngine;
using System;

public class AdsService : IUnityAdsInitializationListener, IUnityAdsShowListener, IAdsService
{
    private const string AndroidGameId = "5662135";
    private const string RewardedAndroid = "Rewarded_Android";
    private const string RewadedPlacmentId = "Rewarded_Android";

    private bool _testMode = true;
    private string _gameId;
    private string _rewardedId;
    private Action _onVideoFinished;

    public bool IsInitialized => Advertisement.isInitialized;
    public string RewardedId => _rewardedId;

    public void Init()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                _gameId = AndroidGameId;

                _rewardedId = RewardedAndroid;
                break;
            case RuntimePlatform.WindowsEditor:
                _gameId = AndroidGameId;

                _rewardedId = RewardedAndroid;
                break;
            default:
                Debug.LogError("Unsupported platform for ads");
                break;
        }

      
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    public void ShowRewardedVideo(Action onVideoFinished)
    {
        Advertisement.Show(RewadedPlacmentId, this);

        _onVideoFinished = onVideoFinished;
    }

    public void OnInitializationComplete() =>
        Debug.Log("Ads complete");

    public void OnInitializationFailed(UnityAdsInitializationError error, string message) =>
        Debug.LogError($"Initialization Failed: {message}");

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
    }

    public void OnUnityAdsShowStart(string placementId)
    {
    }

    public void OnUnityAdsShowClick(string placementId)
    {
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId == RewadedPlacmentId)
            _onVideoFinished?.Invoke();

        _onVideoFinished = null;
    }
}