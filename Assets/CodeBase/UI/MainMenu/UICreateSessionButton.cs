using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;

[RequireComponent(typeof(Button))]
public class UICreateSessionButton : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;

    private Button _thisButton;
    private INetworkProvider _networkProvider;
    private IStaticDataService _staticDataService;
    private IStateMachine _stateMachine;

    [Inject]
    public void Construct(INetworkProvider networkProvider, 
        IStaticDataService staticDataService, 
        INetworkFactory networkFactory,
        IStateMachine stateMachine)
    {
        _networkProvider = networkProvider;
        _staticDataService = staticDataService;
        _stateMachine = stateMachine;
    }

    private void OnEnable()
    {
        _thisButton = GetComponent<Button>();

        _thisButton.onClick.AddListener(CreateGame);
    }

    private void CreateGame()
    {
        _networkProvider.StartGame(_inputField.text, _staticDataService.TestRoomDataHost);

        _stateMachine.Enter<LoadLevelState>();
    }
}
