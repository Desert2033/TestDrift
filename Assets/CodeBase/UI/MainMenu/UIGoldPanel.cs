using UnityEngine;
using TMPro;
using Zenject;
using System;

public class UIGoldPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _goldText;
    
    private IWalletService _walletService;

    [Inject]
    public void Construct(IWalletService walletService)
    {
        _walletService = walletService;
    }

    private void OnEnable()
    {
        _walletService.OnChangeGold += ChangeValue;

        ChangeValue(_walletService.Gold);
    }

    private void OnDisable()
    {
        _walletService.OnChangeGold -= ChangeValue;
    }

    private void ChangeValue(float value)
    {
        _goldText.text = $"{value}";
    }
}
