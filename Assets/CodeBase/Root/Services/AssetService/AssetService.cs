using Fusion;
using UnityEngine;
using Zenject;

public class AssetService : IAssetService
{
    public T Instantiate<T>(string prefabPath, DiContainer container) where T : MonoBehaviour
    {
        T prefab = Resources.Load<T>(prefabPath);

        return container.InstantiatePrefabForComponent<T>(prefab);
    }

    public NetworkObject Instantiate(DiContainer container, NetworkRunner runner, PlayerRef player, NetworkObject prefab, Vector3 position)
    {
        NetworkObject networkPlayerObject = runner.Spawn(prefab, position, Quaternion.identity, player);

        container.Inject(networkPlayerObject);

        MonoBehaviour monoBehaviour = networkPlayerObject.GetComponentInChildren<MonoBehaviour>();
        
        if (monoBehaviour != null)
            container.InjectGameObject(monoBehaviour.gameObject);

        return networkPlayerObject;
    }

    public GameObject Instantiate(GameObject prefab, DiContainer container, Vector3 position, Quaternion rotation) =>
        container.InstantiatePrefab(prefab, position, rotation, null);

    public T Instantiate<T>(string prefabPath, Transform parent, DiContainer container) where T : MonoBehaviour
    {
        T prefab = Resources.Load<T>(prefabPath);

        return container.InstantiatePrefabForComponent<T>(prefab, parent);
    }
}
