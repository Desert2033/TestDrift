using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UICloseButton : MonoBehaviour
{
    [SerializeField] private GameObject _closePanel;
    [SerializeField] private GameObject _openPanel;

    private Button _thisButton;

    private void OnEnable()
    {
        _thisButton = GetComponent<Button>();

        _thisButton.onClick.AddListener(CloseCreateGamePanel);
    }

    private void CloseCreateGamePanel()
    {
        _closePanel.SetActive(false);

        if (_openPanel != null)
            _openPanel.SetActive(true);
    }
}
