using System;

[Serializable]
public class CarData
{
    public CarIds CarId;

    public CarData(CarIds carId)
    {
        CarId = carId;
    }
}
