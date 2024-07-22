using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class UIBackButton : MonoBehaviour
{
    private Button _thisButton;
    private IStateMachine _stateMachine;

    [Inject]
    public void Construct(IStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    private void OnEnable()
    {
        _thisButton = GetComponent<Button>();

        _thisButton.onClick.AddListener(Back);
    }

    private void OnDisable()
    {
        _thisButton.onClick.RemoveListener(Back);
    }

    private void Back()
    {
        _stateMachine.Enter<LoadMainMenuState>();
    }
}
