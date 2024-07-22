using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class UIMenuButton : MonoBehaviour
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

        _thisButton.onClick.AddListener(BackToMenu);
    }

    private void OnDisable()
    {
        _thisButton.onClick.RemoveListener(BackToMenu);
    }

    private void BackToMenu()
    {
        _stateMachine.Enter<LoadMainMenuState>();
    }
}
