using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class UIBuyCashPanel : MonoBehaviour
{
    [SerializeField] private Button _buyButton;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _valueText;
    [SerializeField] private float _price = 100;
    [SerializeField] private float _value = 1000;
    
    private IWalletService _walletService;
    private ISaveLoadService _saveLoadService;

    [Inject]
    public void Construct(IWalletService walletService, ISaveLoadService saveLoadService)
    {
        _walletService = walletService;
        _saveLoadService = saveLoadService;
    }

    private void OnEnable()
    {
        _valueText.text = $"{_value}";
        _priceText.text = $"{_price}";

        _buyButton.onClick.AddListener(BuyCash);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(BuyCash);
    }

    private void BuyCash()
    {
        if (_walletService.Gold >= _price) 
        {
            _walletService.RaiseCash(_value);
            _walletService.DecreaseGold(_price);

            _saveLoadService.SavedProgress();
        }
    }
}
