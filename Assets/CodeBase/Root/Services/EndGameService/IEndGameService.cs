using System;

public interface IEndGameService
{
    event Action OnEndGame;

    void EndGame();
}