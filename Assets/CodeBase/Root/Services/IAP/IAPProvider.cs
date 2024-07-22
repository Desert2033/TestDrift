using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class IAPProvider : IDetailedStoreListener
{
    private IStoreController _controller;
    private IExtensionProvider _extensions;
    private IAPService _iapService;

    public Dictionary<string, ProductConfig> Configs { get; private set; }
    public bool IsInitialized => _controller != null && _extensions != null;

    public event Action Initialized;

    public void Initialize(IAPService iapService)
    {
        _iapService = iapService;

        Configs = new Dictionary<string, ProductConfig>();

        ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        LoadProductConfigs();
        FillBuilderProducts(builder);

        UnityPurchasing.Initialize(this, builder);
    }

    public void StartPurchase(string productId) => 
        _controller.InitiatePurchase(productId);

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        _controller = controller;
        _extensions = extensions;

        Initialized?.Invoke();

        Debug.Log("UnityPurchasing initialization success !");
    }

    public void OnInitializeFailed(InitializationFailureReason error) => 
        Debug.LogError($"UnityPurchasing OnInitializeFailed : {error}");

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {

    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) => 
        Debug.LogError($"Product {product.definition.id} purchase failed, PurchaseFailureDescription: {failureReason}, transaction id: {product.transactionID} ");

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        Debug.Log($"UnityPurchasing purchase success productId: {purchaseEvent.purchasedProduct.definition.id}");
        
        return _iapService.ProcessPurchase(purchaseEvent.purchasedProduct);
    }

    private void FillBuilderProducts(ConfigurationBuilder builder)
    {
        foreach (ProductConfig config in Configs.Values)
        {
            builder.AddProduct(config.Id, config.ProductType);
        }
    }

    private void LoadProductConfigs() => 
        Configs = Resources.Load<TextAsset>(AssetPaths.IAPConfigsPath)
        .text
        .ToDeserialized<ProductConfigWrapper>()
        .Configs
        .ToDictionary(x => x.Id, x => x);
}
