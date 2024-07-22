using System.Collections.Generic;
using UnityEngine;

public class GarageService : IGarageService
{
    private ISaveLoadService _saveLoadService;
    private IPersistentProgressService _progressService;

    public List<CarData> CarsInGarage { get; private set; }
    public CarData TakedCar { get; private set; }


    public GarageService(IPersistentProgressService progressService)
    {
        CarsInGarage = progressService.Progress.CarGarageData.CarDatas;
        TakedCar = progressService.Progress.CarGarageData.CarCurrent;

        _progressService = progressService;
    }

    public bool ContainsCarInGarage(CarIds carId) =>
        CarsInGarage.Exists((car) => car.CarId == carId);

    public void AddNewCar(CarIds carId)
    {
        CarsInGarage.Add(new CarData(carId));
    }

    public void ChangeTakedCard(CarIds carId)
    {
        TakedCar = new CarData(carId);

        _progressService.Progress.CarGarageData.CarCurrent = TakedCar;
    }

    public bool IsCarCurrent(CarIds carId) =>
        TakedCar.CarId == carId;
}
