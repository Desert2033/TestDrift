using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class UITakeButton : MonoBehaviour
{
    [SerializeField] private ConveyorCarBehaviour _conveyorCar;

    private Button _thisButton;
    private IGarageService _garageService;
    private ISaveLoadService _saveLoadService;

    [Inject]
    public void Construct(IGarageService garageService, ISaveLoadService saveLoadService)
    {
        _garageService = garageService;
        _saveLoadService = saveLoadService;
    }

    private void Awake()
    {
        _thisButton = GetComponent<Button>();

        _thisButton.onClick.AddListener(TakeCar);
        _conveyorCar.OnCarSpawn += ChangeCurrentCar;
    }


    private void OnDestroy()
    {
        _thisButton.onClick.RemoveListener(TakeCar);
        _conveyorCar.OnCarSpawn -= ChangeCurrentCar;
    }

    private void ChangeCurrentCar(CarIds carId)
    {
        ActiveDisactiveThisButton(carId);
    }

    private void TakeCar()
    {
        _garageService.ChangeTakedCard(_conveyorCar.CurrentCar);

        ActiveDisactiveThisButton(_conveyorCar.CurrentCar);

        _saveLoadService.SavedProgress();
    }
    
    private void ActiveDisactiveThisButton(CarIds carId)
    {
        if (_garageService.IsCarCurrent(carId))
            _thisButton.interactable = false;
        else
            _thisButton.interactable = true;
    }
}
