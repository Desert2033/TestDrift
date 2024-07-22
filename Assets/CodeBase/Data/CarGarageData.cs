using System;
using System.Collections.Generic;

[Serializable]
public class CarGarageData
{
    public List<CarData> CarDatas = new List<CarData>();
    public CarData CarCurrent;

    public CarGarageData()
    {
        CarDatas.Add(new CarData(CarIds.ModelDefault));
        CarCurrent = CarDatas[0];
    }
}
