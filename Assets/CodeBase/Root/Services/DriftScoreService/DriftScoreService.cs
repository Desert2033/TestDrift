using System;

public class DriftScoreService : IDriftScoreService
{
    public float TotalScore { get; private set; }
    public float WinScore { get; private set; }

    public event Action<float> OnChangeWinScore;

    public void AddTotalScore(float score)
    {
        TotalScore += score;
    }

    public void SetTotalScoreZero()
    {
        TotalScore = 0;
    }

    public void SetWinScore(float score)
    {
        WinScore = score;

        OnChangeWinScore?.Invoke(WinScore);
    }

    public void DoubleWinScore()
    {
        WinScore *= 2;

        OnChangeWinScore?.Invoke(WinScore);
    }
}
