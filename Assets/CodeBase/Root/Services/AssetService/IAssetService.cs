using Fusion;
using UnityEngine;
using Zenject;

public interface IAssetService : IService
{
    GameObject Instantiate(GameObject prefab, DiContainer container, Vector3 position, Quaternion rotation);
    NetworkObject Instantiate(DiContainer container, NetworkRunner runner, PlayerRef player, NetworkObject prefab, Vector3 position);
    T Instantiate<T>(string prefabPath, DiContainer container) where T : MonoBehaviour;
    T Instantiate<T>(string prefabPath, Transform parent, DiContainer container) where T : MonoBehaviour;
}