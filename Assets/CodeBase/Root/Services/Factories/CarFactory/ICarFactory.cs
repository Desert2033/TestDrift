using Fusion;
using UnityEngine;

public interface ICarFactory : IService
{
    GameObject CreateCar(GameObject prefab, Vector3 position, Quaternion rotation);
    NetworkObject CreateCar(NetworkRunner runner, PlayerRef player, NetworkObject carPrefab, Vector3 position);
}