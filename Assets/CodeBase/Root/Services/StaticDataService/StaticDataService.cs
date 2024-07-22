using System.Collections.Generic;
using UnityEngine;

public class StaticDataService : IStaticDataService
{
    public List<CarStaticData> CarStaticDatas { get; } = new List<CarStaticData>();

    public TestRoomData TestRoomDataHost { get; private set; }
    public TestRoomData TestRoomDataClient { get; private set; }

    public void LoadRoomData()
    {
        TestRoomDataHost = Resources.Load<TestRoomData>(AssetPaths.RoomDataPath);
        TestRoomDataClient = Resources.Load<TestRoomData>(AssetPaths.RoomDataClientPath);
    }

    public void LoadCarsData() =>
        CarStaticDatas.AddRange(Resources.LoadAll<CarStaticData>(AssetPaths.CarStaticDatasPath));

    public CarStaticData GetCarById(CarIds carId) =>
        CarStaticDatas.Find((carData) => carData.CarId == carId);
}