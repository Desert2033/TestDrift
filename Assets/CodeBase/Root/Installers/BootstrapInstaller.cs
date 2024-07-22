using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //base.InstallBindings();

        BindSaveLoadService();

        BindPersistentProgressService();

        BindSceneLoader();

        BindCoroutineRunner();

        BindGameStateMachine();

        BindApiDataService();

        BindAssetService();

        BindNetworkFactory();

        BindUIFactory();

        BindCarFactory();

        BindNetworkProvider();

        BindGarageService();

        BindWalletService();

        BindRegisterProgressReaders();

        BindIAPService();

        BindInputService();

        BindCarSpawnPositionService();

        BindTimeOverService();

        BindDriftScoreService();

        BindEndGameService();
        
        BindAdsService();
    }

    private void BindAdsService() => 
        Container.BindInterfacesAndSelfTo<AdsService>().AsSingle();

    private void BindEndGameService() => 
        Container.BindInterfacesAndSelfTo<EndGameService>().AsSingle();

    private void BindDriftScoreService() => 
        Container.BindInterfacesAndSelfTo<DriftScoreService>().AsSingle();

    private void BindTimeOverService() => 
        Container.BindInterfacesAndSelfTo<TimeOverService>().AsSingle();

    private void BindCarSpawnPositionService() => 
        Container.BindInterfacesAndSelfTo<CarSpawnPositionService>().AsSingle();

    private void BindInputService() => 
        Container.BindInterfacesAndSelfTo<InputService>().AsSingle();

    private void BindIAPService() => 
        Container.BindInterfacesAndSelfTo<IAPService>().AsSingle();

    private void BindWalletService() => 
        Container.BindInterfacesAndSelfTo<WalletService>().AsSingle();

    private void BindGarageService() => 
        Container.BindInterfacesAndSelfTo<GarageService>().AsSingle();

    private void BindRegisterProgressReaders() => 
        Container.BindInterfacesAndSelfTo<RegisterProgressReadersService>().AsSingle();

    private void BindCarFactory() => 
        Container.BindInterfacesAndSelfTo<CarFactory>().AsSingle();

    private void BindUIFactory() => 
        Container.BindInterfacesAndSelfTo<UIFactory>().AsSingle();

    private void BindNetworkProvider() => 
        Container.BindInterfacesAndSelfTo<NetworkProvider>().AsSingle();

    private void BindNetworkFactory() => 
        Container.BindInterfacesAndSelfTo<NetworkFactory>().AsSingle();

    private void BindAssetService() => 
        Container.BindInterfacesAndSelfTo<AssetService>().AsSingle();

    private void BindApiDataService() => 
        Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle();

    private void BindSceneLoader() =>
        Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();

    private void BindCoroutineRunner() => 
        Container.Bind<ICoroutineRunner>().To<CoroutineRunner>().FromComponentInNewPrefabResource(AssetPaths.CoroutineRunnerPath).AsSingle();

    private void BindPersistentProgressService() => 
        Container.BindInterfacesAndSelfTo<PersistentProgressService>().AsSingle();

    private void BindSaveLoadService() => 
        Container.BindInterfacesAndSelfTo<SaveLoadService>().AsSingle();

    private void BindGameStateMachine() => 
        Container.BindInterfacesAndSelfTo<StateMachine>().AsSingle();
}
