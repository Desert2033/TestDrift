using UnityEngine;
using Zenject;
using TMPro;
using UnityEngine.UI;
using System;

public class UIPackPanel : MonoBehaviour
{
    [SerializeField] private string _productId;
    [SerializeField] private TextMeshProUGUI _goldValueText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private Button _buyButton;

    private IIAPService _iapService;
    private ISaveLoadService _saveLoadService;
    private ProductConfig _thisProduct;

    [Inject]
    public void Construct(IIAPService iapService, ISaveLoadService saveLoadService)
    {
        _iapService = iapService;
        _saveLoadService = saveLoadService;
    }

    private void OnEnable()
    {
        _thisProduct = _iapService.GetProductById(_productId);

        _goldValueText.text = $"{_thisProduct.Gold}";
        _priceText.text = $"{_thisProduct.Price} $";

        _buyButton.onClick.AddListener(BuyPack);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(BuyPack);
    }

    private void BuyPack()
    {
        _iapService.StartPurchase(_thisProduct.Id);
        
        _saveLoadService.SavedProgress();
    }
}
