using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Fusion;
using System;
using Zenject;

public class SessionItemInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _sessionNameText;
    [SerializeField] private TextMeshProUGUI _playerCountText;
    [SerializeField] private Button _joinButton;

    private SessionInfo _sessionInfo;
    private INetworkProvider _networkProvider;
    private IStateMachine _stateMachine;

    [Inject]
    public void Construct(INetworkProvider networkProvider, IStateMachine stateMachine)
    {
        _networkProvider = networkProvider;
        _stateMachine = stateMachine;
    }

    private void OnEnable()
    {
        _joinButton.onClick.AddListener(OnClickJoin);
    }

    private void OnDisable()
    {
        _joinButton.onClick.RemoveListener(OnClickJoin);
    }

    public void SetInformation(SessionInfo sessionInfo)
    {
        _sessionInfo = sessionInfo;

        _sessionNameText.text = sessionInfo.Name;
        _playerCountText.text = $"{sessionInfo.PlayerCount}/{sessionInfo.MaxPlayers}";

        if (sessionInfo.PlayerCount >= sessionInfo.MaxPlayers)
            _joinButton.gameObject.SetActive(false);
        else
            _joinButton.gameObject.SetActive(true);
    }

    public void OnClickJoin()
    {
        _networkProvider.JoinGame(_sessionInfo);

        _stateMachine.Enter<LoadLevelState>();
    }
}
