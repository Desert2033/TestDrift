using Fusion;
using UnityEngine;
using Zenject;

public class Car : NetworkBehaviour
{
    [SerializeField] private GameObject _carDefault;
    [SerializeField] private GameObject _car1;
    [SerializeField] private GameObject _car2;

    private IGarageService _garageService;

    [Networked] public CarIds CurrentCarId { get; set; }

    [Inject]
    public void Construct(IGarageService garageService)
    {
        _garageService = garageService;
    }

    public override void Spawned()
    {
        base.Spawned();
        
        ChooseCar(CurrentCarId);

        if (Object.HasInputAuthority)
        {
            ProjectContext.Instance.Container.InjectGameObject(gameObject);

            CurrentCarId = _garageService.TakedCar.CarId;

            RPC_ChangeCar(CurrentCarId);
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void RPC_ChangeCar(CarIds carId, RpcInfo rpcInfo = default)
    {
        CurrentCarId = carId;

        ChooseCar(CurrentCarId);
    }

    private void ChooseCar(CarIds carId)
    {
        _carDefault.SetActive(false);
        _car1.SetActive(false);
        _car2.SetActive(false);

        switch (carId)
        {
            case CarIds.ModelDefault:
                _carDefault.SetActive(true);
                break;
            case CarIds.Model1:
                _car1.SetActive(true);
                break;
            case CarIds.Model2:
                _car2.SetActive(true);
                break;
        }
    }

}
