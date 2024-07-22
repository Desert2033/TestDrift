using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIButtonChoosePanel : MonoBehaviour
{
    [SerializeField] private ConveyorCarBehaviour _conveyorCar;
    [SerializeField] private GameObject _buyPanel;
    [SerializeField] private Button _takeButton;
    [SerializeField] private UIBuyCarButton _buyCarButton;

    private IGarageService _garageService;

    [Inject]
    public void Construct(IGarageService garageService)
    {
        _garageService = garageService;
    }

    private void OnEnable()
    {
        _conveyorCar.OnCarSpawn += ChooseButton;
        _buyCarButton.OnBuyCar += ChooseButton;
    }

    private void OnDisable()
    {
        _conveyorCar.OnCarSpawn -= ChooseButton;
        _buyCarButton.OnBuyCar -= ChooseButton;
    }

    private void ChooseButton(CarIds carId)
    {
        if (_garageService.ContainsCarInGarage(carId))
            ActiveTakeButton();
        else
            ActiveBuyPanel();
    }

    private void ActiveBuyPanel()
    {
        _takeButton.gameObject.SetActive(false);
        _buyPanel.gameObject.SetActive(true);
    }

    private void ActiveTakeButton()
    {
        _takeButton.gameObject.SetActive(true);
        _buyPanel.gameObject.SetActive(false);
    }
}
