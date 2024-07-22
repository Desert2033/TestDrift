using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class UICarShopButton : MonoBehaviour
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

        _thisButton.onClick.AddListener(OpenCarShop);
    }

    private void OpenCarShop()
    {
        _stateMachine.Enter<LoadCarShopState>();
    }
}
