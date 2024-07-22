using System;

public class EndGameService : IEndGameService
{
    public event Action OnEndGame;

    public void EndGame()
    {
        OnEndGame?.Invoke();
    }
}
