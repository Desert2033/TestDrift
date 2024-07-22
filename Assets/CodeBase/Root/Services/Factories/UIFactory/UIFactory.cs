using UnityEngine;
using Zenject;

public class UIFactory : IUIFactory
{
    private IAssetService _assetService;
    private DiContainer _container;

    public UIFactory(DiContainer container, IAssetService assetService)
    {
        _assetService = assetService;
        _container = container;
    }

    public SessionItemInfoUI CreateSessionItem(Transform parent) =>
        _assetService.Instantiate<SessionItemInfoUI>(AssetPaths.SessionItemPath, parent, _container);
}
