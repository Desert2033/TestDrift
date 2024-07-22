using UnityEngine;
using Zenject;
using TMPro;
using System;

public class UIFinishPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textTotalScore;
    [SerializeField] private TextMeshProUGUI _textCash;

    private IDriftScoreService _driftScoreService;

    [Inject]
    public void Construct(IDriftScoreService driftScoreService)
    {
        _driftScoreService = driftScoreService;
    }

    private void OnEnable()
    {
        _driftScoreService.SetWinScore(_driftScoreService.TotalScore);

        _textTotalScore.text = $"{(int)_driftScoreService.TotalScore}";
        ChangeWinScore(_driftScoreService.WinScore);

        _driftScoreService.OnChangeWinScore += ChangeWinScore;
    }

    private void OnDisable()
    {
        _driftScoreService.OnChangeWinScore -= ChangeWinScore;
    }

    private void ChangeWinScore(float score)
    {
        _textCash.text = $"{(int)score}";
    }
}
