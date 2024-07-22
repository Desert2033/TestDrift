using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class UIBuyCarButton : MonoBehaviour
{
    [SerializeField] private ConveyorCarBehaviour _conveyorCar;
    [SerializeField] private GameObject _noMoneyPanel;

    private Button _thisButton;
    private IGarageService _garageService;
    private IWalletService _walletService;
    private IStaticDataService _staticDataService;
    private ISaveLoadService _saveLoadService;
    
    public Action<CarIds> OnBuyCar;

    [Inject]
    public void Construct(IGarageService garageService, 
        IWalletService walletService, 
        IStaticDataService staticDataService,
        ISaveLoadService saveLoadService)
    {
        _garageService = garageService;
        _walletService = walletService;
        _staticDataService = staticDataService;
        _saveLoadService = saveLoadService;
    }

    private void Start()
    {
        _thisButton = GetComponent<Button>();

        _thisButton.onClick.AddListener(BuyCar);
    }

    private void OnDestroy()
    {
        _thisButton.onClick.RemoveListener(BuyCar);
    }

    private void BuyCar()
    {
        CarStaticData carData = _staticDataService.GetCarById(_conveyorCar.CurrentCar);

        if (!_garageService.ContainsCarInGarage(_conveyorCar.CurrentCar) && _walletService.Cash >= carData.Price)
        {
            _walletService.DecreaseCash(carData.Price);

            _garageService.AddNewCar(_conveyorCar.CurrentCar);

            OnBuyCar?.Invoke(_conveyorCar.CurrentCar);
            _saveLoadService.SavedProgress();
        }
        else
        {
            StartCoroutine(ShowNoMoneyCoroutine());
        }
    }

    private IEnumerator ShowNoMoneyCoroutine()
    {
        _noMoneyPanel.SetActive(true);

        yield return new WaitForSeconds(1f);

        _noMoneyPanel.SetActive(false);
    }
}
