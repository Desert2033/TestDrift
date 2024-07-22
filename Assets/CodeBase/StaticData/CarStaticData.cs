using UnityEngine;

public enum CarIds : byte
{
    ModelDefault,
    Model1,
    Model2,
}

[CreateAssetMenu(fileName = "CarData", menuName = "StaticData/CarData")]
public class CarStaticData : ScriptableObject
{
    public CarIds CarId;
    public GameObject Prefab;
    public float Price;
}
