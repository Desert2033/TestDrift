using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIIAPShopButton : MonoBehaviour
{
    [SerializeField] private GameObject _iapPanel;
    [SerializeField] private GameObject _mainButtonsPanel;

    private Button _thisButton;

    private void OnEnable()
    {
        _thisButton = GetComponent<Button>();

        _thisButton.onClick.AddListener(OpenIAPPanel);
    }

    private void OnDisable()
    {
        _thisButton.onClick.RemoveListener(OpenIAPPanel);
    }

    private void OpenIAPPanel()
    {
        _iapPanel.SetActive(true);
        _mainButtonsPanel.SetActive(false);
    }
}
