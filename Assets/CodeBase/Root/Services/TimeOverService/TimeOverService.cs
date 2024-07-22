using System;

public class TimeOverService : ITimeOverService
{
    public float TimeOver { get; private set; }

    public event Action<float> OnTimeOverChange;

    public void SetTimeOver(float time)
    {
        TimeOver = time;

        OnTimeOverChange?.Invoke(TimeOver);
    }

    public void AddTime(float time)
    {
        TimeOver += time;

        OnTimeOverChange?.Invoke(TimeOver);
    }

    public void RemoveTime(float time)
    {
        TimeOver -= time;

        OnTimeOverChange?.Invoke(TimeOver);
    }
}
