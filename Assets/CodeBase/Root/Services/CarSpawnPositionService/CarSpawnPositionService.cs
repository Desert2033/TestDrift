using System.Collections.Generic;
using UnityEngine;

public class CarSpawnPositionService : ICarSpawnPositionService
{
    private List<Transform> _spawnCarPositions = new List<Transform>();

    public void AddSpawnPostion(Transform position)
    {
        _spawnCarPositions.Add(position);
    }

    public Vector3 GetSpawnPosition()
    {
        foreach (Transform spawnPosition in _spawnCarPositions)
        {
            Vector3 position = spawnPosition.position;

            _spawnCarPositions.Remove(spawnPosition);
            return position;
        }

        return Vector3.zero;
    }

    public void Clear()
    {
        _spawnCarPositions.Clear();
    }
}
