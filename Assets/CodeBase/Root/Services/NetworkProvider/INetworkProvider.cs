using Fusion;
using System;

public interface INetworkProvider : IService
{
    event Action<NetworkRunner> OnGameStarted;

    void JoinGame(SessionInfo sessionInfo);
    void StartGame(string sessionName,TestRoomData testRoomData);
    void JoinLobby();
    bool IsServer();
    void Shutdown();
}