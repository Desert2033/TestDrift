using System;
using UnityEngine.Purchasing;

public interface IIAPService : IService
{
    bool IsInitialized { get; }

    event Action Initialized;

    ProductConfig GetProductById(string id);
    void Initialize();
    PurchaseProcessingResult ProcessPurchase(Product purchaseProduct);
    void StartPurchase(string productId);
}