using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class NetworkRunnerCallbacks : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private NetworkObject _playerPrefab;

    private Dictionary<PlayerRef, NetworkObject> _spawnedObjects = new Dictionary<PlayerRef, NetworkObject>();
    private IInputService _inputService;
    private ICarFactory _carFactory;
    private ICarSpawnPositionService _carSpawnPosition;
    private IEndGameService _endGameService;
    private IGarageService _garageService;
    private CarIds _carId;

    [Inject]
    public void Construct(IInputService inputService,
        ICarFactory carFactory,
        IEndGameService endGameService,
        ICarSpawnPositionService carSpawnPosition,
        IGarageService garageService)
    {
        _inputService = inputService;
        _carFactory = carFactory;
        _carSpawnPosition = carSpawnPosition;
        _endGameService = endGameService;
        _garageService = garageService;
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        NetworkInputData data = new NetworkInputData();

        data.VerticalInput = _inputService.GetVerticalInput();
        data.HorizontalInput = _inputService.GetHorizontalInput();
        data.BrakeInput = _inputService.GetBrakeInput();

        input.Set(data);
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {
            CreatePlayer(runner, player);
        }

    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        RemovePlayer(runner, player);
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        SessionInfoListUI sessionListUI = FindAnyObjectByType<SessionInfoListUI>(FindObjectsInactive.Include);

        Debug.Log("sessionListUI: " + sessionListUI);

        if (sessionList.Count != 0)
        {
            sessionListUI.ClearList();

            foreach (var item in sessionList)
            {
                sessionListUI.AddItem(item);
            }
        }
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        _endGameService.EndGame();
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
    }

    private void CreatePlayer(NetworkRunner runner, PlayerRef player)
    {
        NetworkObject car = _carFactory.CreateCar(runner, player, _playerPrefab, _carSpawnPosition.GetSpawnPosition());

        _spawnedObjects.Add(player, car);
    }

    private void RemovePlayer(NetworkRunner runner, PlayerRef player)
    {
        

        if (_spawnedObjects.TryGetValue(player, out NetworkObject networkObject))
        {
            runner.Despawn(networkObject);
            _spawnedObjects.Remove(player);
        }
    }
}
