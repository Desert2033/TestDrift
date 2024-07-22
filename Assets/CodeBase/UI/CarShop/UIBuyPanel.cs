using UnityEngine;
using TMPro;
using System;
using Zenject;

public class UIBuyPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private ConveyorCarBehaviour _conveyorCar;
    
    private IStaticDataService _staticDataService;

    [Inject]
    public void Construct(IStaticDataService staticDataService)
    {
        _staticDataService = staticDataService;

        _conveyorCar.OnCarSpawn += ChangePrice;
    }

    private void OnDestroy()
    {
        _conveyorCar.OnCarSpawn -= ChangePrice;
    }

    private void ChangePrice(CarIds carId)
    {
        CarStaticData carData = _staticDataService.GetCarById(carId);

        _priceText.text = $"{carData.Price}";
    }
}
