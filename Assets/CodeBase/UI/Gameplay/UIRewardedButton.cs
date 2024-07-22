using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class UIRewardedButton : MonoBehaviour
{
    private Button _thisButton;
    private IAdsService _adsService;
    private IDriftScoreService _driftScoreService;

    [Inject]
    public void Construct(IAdsService adsService, IDriftScoreService driftScoreService)
    {
        _adsService = adsService;
        _driftScoreService = driftScoreService;
    }

    private void OnEnable()
    {
        _thisButton = GetComponent<Button>();

        _thisButton.onClick.AddListener(StartRewarded);
    }

    private void StartRewarded()
    {
        _adsService.ShowRewardedVideo(RaiseCash);
        gameObject.SetActive(false);
    }

    private void RaiseCash()
    {
        _driftScoreService.DoubleWinScore();
    }
}
