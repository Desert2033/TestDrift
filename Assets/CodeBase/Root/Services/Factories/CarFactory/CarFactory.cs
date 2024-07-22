using Fusion;
using UnityEngine;
using Zenject;

public class CarFactory : ICarFactory
{
    private IAssetService _assetService;
    private DiContainer _diContainer;

    public CarFactory(DiContainer diContainer, IAssetService assetService)
    {
        _assetService = assetService;
        _diContainer = diContainer;
    }

    public GameObject CreateCar(GameObject prefab, Vector3 position, Quaternion rotation) =>
        _assetService.Instantiate(prefab, _diContainer, position, rotation);

    public NetworkObject CreateCar(NetworkRunner runner, PlayerRef player, NetworkObject carPrefab, Vector3 position) =>
        _assetService.Instantiate(_diContainer, runner, player, carPrefab, position);
}
