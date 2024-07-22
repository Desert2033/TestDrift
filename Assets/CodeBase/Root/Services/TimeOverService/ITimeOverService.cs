using System;

public interface ITimeOverService
{
    float TimeOver { get; }

    event Action<float> OnTimeOverChange;

    void AddTime(float time);
    void RemoveTime(float time);
    void SetTimeOver(float time);
}