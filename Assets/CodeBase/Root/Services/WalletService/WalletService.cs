using System;
using UnityEngine;

public class WalletService : IWalletService
{
    private IPersistentProgressService _progressService;

    public float Gold => _progressService.Progress.WalletData.Gold;
    public float Cash => _progressService.Progress.WalletData.Cash;

    public event Action<float> OnChangeGold;
    public event Action<float> OnChangeCash;

    public WalletService(IPersistentProgressService progressService)
    {
        _progressService = progressService;
    }

    public void RaiseGold(float value)
    {
        _progressService.Progress.WalletData.Gold += value;

        OnChangeGold?.Invoke(Gold);
    }

    public void RaiseCash(float value)
    {
        _progressService.Progress.WalletData.Cash += value;

        OnChangeCash?.Invoke(Cash);
    }

    public void DecreaseGold(float value)
    {
        _progressService.Progress.WalletData.Gold -= value;

        OnChangeGold?.Invoke(Gold);
    }

    public void DecreaseCash(float value)
    {
        _progressService.Progress.WalletData.Cash -= value;

        OnChangeCash?.Invoke(Cash);
    }
}
