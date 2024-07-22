using Fusion;
using Fusion.Photon.Realtime;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkProvider : INetworkProvider
{
    private const string RootLobbyName = "RootLobby";

    private NetworkRunner _networkRunner;
    private INetworkFactory _networkFactory;

    public event Action<NetworkRunner> OnGameStarted;

    public NetworkProvider(INetworkFactory networkFactory)
    {
        _networkFactory = networkFactory;
    }

    async public void StartGame(string sessionName, TestRoomData testRoomData)
    {
        await CheckNetworkRunner();

        _networkRunner.ProvideInput = true;
        
        SceneRef scene = SceneRef.FromIndex(testRoomData.SceneIndex);
        NetworkSceneInfo sceneInfo = new NetworkSceneInfo();

        if (scene.IsValid)
        {
            sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);
        }

        await _networkRunner.StartGame(new StartGameArgs()
        {
            GameMode = testRoomData.GameMode,
            SessionName = sessionName,
            Scene = scene,
            CustomLobbyName = RootLobbyName,
            SceneManager = _networkRunner.gameObject.AddComponent<NetworkSceneManagerDefault>(),
            OnGameStarted = GameStarted,
        });
    }

    async public void JoinGame(SessionInfo sessionInfo)
    {
        await CheckNetworkRunner();

        SceneRef scene = SceneRef.FromIndex(2);
        NetworkSceneInfo sceneInfo = new NetworkSceneInfo();

        if (scene.IsValid)
        {
            sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);
        }

        await _networkRunner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Client,
            SessionName = sessionInfo.Name,
            Scene = scene,
            CustomLobbyName = RootLobbyName,
            SceneManager = _networkRunner.gameObject.AddComponent<NetworkSceneManagerDefault>(),
            OnGameStarted = GameStarted,
        });
    }

    async public void JoinLobby()
    {
        await CheckNetworkRunner();

        Debug.Log("Lobby Start");

        string lobbyID = "RootLobby";

        StartGameResult result = await _networkRunner.JoinSessionLobby(SessionLobby.Custom, lobbyID);

        if (result.Ok)
            Debug.Log("JOIN LOBBY OK !!!");
        else
            Debug.LogError($"ERROR JOIN LOBBY {lobbyID}");
    }

    public void Shutdown() => 
        _networkRunner.Shutdown();

    public bool IsServer() =>
        _networkRunner.IsServer;

    private void GameStarted(NetworkRunner runner) => 
        OnGameStarted?.Invoke(runner);

    private async Task CheckNetworkRunner()
    {
        if (_networkRunner != null)
        {
            await _networkRunner.Shutdown();
            _networkRunner = _networkFactory.CreateNetworkRunner();
        }
        else
        {
            _networkRunner = _networkFactory.CreateNetworkRunner();
        }
    }
}
