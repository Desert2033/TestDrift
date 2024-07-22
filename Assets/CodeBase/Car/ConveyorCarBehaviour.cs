using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ConveyorCarBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _carPointPosition;
    [SerializeField] private Button _arrowNext;
    [SerializeField] private Button _arrowPrev;

    private List<CarIds> _cars;
    private CarIds _currentCar;
    private GameObject _currentCarObject;
    private IStaticDataService _staticDataService;
    private ICarFactory _carFactory;

    public CarIds CurrentCar => _currentCar;
    
    public Action<CarIds> OnCarSpawn;

    [Inject]
    public void Construct(IStaticDataService staticDataService, 
        ICarFactory carFactory)
    {
        _staticDataService = staticDataService;
        _carFactory = carFactory;
    }

    private void Start()
    {
        FillShop();
        SpawnFirstCar();
    }

    private void OnEnable()
    {
        _arrowNext.onClick.AddListener(MoveNextCar);
        _arrowPrev.onClick.AddListener(MovePrevCar);
    }

    private void OnDisable()
    {
        _arrowNext.onClick.RemoveListener(MoveNextCar);
        _arrowPrev.onClick.RemoveListener(MovePrevCar);
    }

    private void SpawnFirstCar() => 
        SpawnCar(CarIds.ModelDefault);

    private void FillShop()
    {
        _cars = new List<CarIds>();

        foreach (CarStaticData carData in _staticDataService.CarStaticDatas)
        {
            _cars.Add(carData.CarId);
        }
    }

    private void MovePrevCar()
    {
        int indexCurrentCar = _cars.IndexOf(_currentCar);
        int indexPrevCar = indexCurrentCar - 1 < 0 ? _cars.Count - 1 : indexCurrentCar - 1;

        SpawnCar(_cars[indexPrevCar]);
    }

    private void MoveNextCar()
    {
        int indexCurrentCar = _cars.IndexOf(_currentCar);
        int indexPrevCar = indexCurrentCar + 1 >= _cars.Count ? 0 : indexCurrentCar + 1;

        SpawnCar(_cars[indexPrevCar]);
    }

    private void SpawnCar(CarIds carId)
    {
        CarStaticData carData = _staticDataService.GetCarById(carId);

        if (_currentCarObject != null)
            Destroy(_currentCarObject);

        _currentCarObject = Instantiate(carData.Prefab, _carPointPosition.position, _carPointPosition.rotation, null);

        // _currentCarObject = _carFactory.CreateCar(carData.Prefab, _carPointPosition.position, _carPointPosition.rotation); NEED FIX !!!

        _currentCar = carId;

        OnCarSpawn?.Invoke(carId);
    }
}
