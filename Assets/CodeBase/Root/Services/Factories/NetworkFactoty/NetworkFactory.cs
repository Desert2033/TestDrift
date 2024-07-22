using Fusion;
using Zenject;

public class NetworkFactory : INetworkFactory
{
    private IAssetService _assetService;
    private DiContainer _container;

    public NetworkFactory(DiContainer container, IAssetService assetService)
    {
        _assetService = assetService;
        _container = container;
    }

    public NetworkRunner CreateNetworkRunner()
    {
        NetworkRunner networkRunner = _assetService.Instantiate<NetworkRunner>(AssetPaths.NetworkRunnerPath, _container);

        networkRunner.ProvideInput = true;
        
        return networkRunner;
    }
}
