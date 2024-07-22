using System.Collections.Generic;

public interface IGarageService : IService
{
    List<CarData> CarsInGarage { get; }
    CarData TakedCar { get; }

    void AddNewCar(CarIds carId);
    void ChangeTakedCard(CarIds carId);
    bool ContainsCarInGarage(CarIds carId);
    bool IsCarCurrent(CarIds carId);
}