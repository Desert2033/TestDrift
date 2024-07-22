using UnityEngine;

public interface ICarSpawnPositionService : IService
{
    void AddSpawnPostion(Transform position);
    void Clear();
    Vector3 GetSpawnPosition();
}