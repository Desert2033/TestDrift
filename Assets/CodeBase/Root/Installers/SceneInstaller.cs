using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private Transform _carSpawnPosition1;
    [SerializeField] private Transform _carSpawnPosition2;
    [SerializeField] private CameraVirtual _cameraVirtual;
    [SerializeField] private UITopDriftPanel _uiDriftPanel;

    private ICarSpawnPositionService _carSpawnPosition;

    [Inject]
    public void Construct(ICarSpawnPositionService carSpawnPosition)
    {
        _carSpawnPosition = carSpawnPosition;
    }

    public override void InstallBindings()
    {
        _carSpawnPosition.AddSpawnPostion(_carSpawnPosition1);
        _carSpawnPosition.AddSpawnPostion(_carSpawnPosition2);
    }
}
