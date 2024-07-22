using System.Collections.Generic;

public interface IStaticDataService : IService
{
    TestRoomData TestRoomDataHost { get; }
    TestRoomData TestRoomDataClient { get; }
    List<CarStaticData> CarStaticDatas { get; }
    void LoadRoomData();
    void LoadCarsData();
    CarStaticData GetCarById(CarIds carModel);
}
