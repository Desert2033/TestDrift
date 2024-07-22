using System;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPService : IIAPService
{
    private IAPProvider _iapProvider;
    private IWalletService _walletService;

    public bool IsInitialized => _iapProvider.IsInitialized;

    public event Action Initialized;

    public IAPService(IWalletService walletService)
    {
        _iapProvider = new IAPProvider();
        _walletService = walletService;
    }

    public void Initialize()
    {
        _iapProvider.Initialize(this);
        _iapProvider.Initialized += () => Initialized?.Invoke();
    }

    public void StartPurchase(string productId) =>
        _iapProvider.StartPurchase(productId);

    public ProductConfig GetProductById(string id) =>
        _iapProvider.Configs[id];

    public PurchaseProcessingResult ProcessPurchase(Product purchaseProduct)
    {
        ProductConfig productConfig = _iapProvider.Configs[purchaseProduct.definition.id];

        _walletService.RaiseGold(productConfig.Gold);

        return PurchaseProcessingResult.Complete;
    }
}
