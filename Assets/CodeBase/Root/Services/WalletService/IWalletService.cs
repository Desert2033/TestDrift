using System;

public interface IWalletService : IService
{
    float Cash { get; }
    float Gold { get; }

    event Action<float> OnChangeGold;
    event Action<float> OnChangeCash;

    void DecreaseCash(float value);
    void DecreaseGold(float value);
    void RaiseCash(float value);
    void RaiseGold(float value);
}