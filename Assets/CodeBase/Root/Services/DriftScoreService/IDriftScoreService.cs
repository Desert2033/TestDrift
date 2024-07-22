using System;

public interface IDriftScoreService : IService
{
    float TotalScore { get; }
    float WinScore { get; }

    event Action<float> OnChangeWinScore;

    void AddTotalScore(float score);
    void DoubleWinScore();
    void SetTotalScoreZero();
    void SetWinScore(float score);
}