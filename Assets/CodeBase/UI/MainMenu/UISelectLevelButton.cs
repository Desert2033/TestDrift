using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UISelectLevelButton : MonoBehaviour
{
    [SerializeField] private GameObject _buttonPanel;
    [SerializeField] private GameObject _sessionTable;

    private Button _thisButton;

    private void OnEnable()
    {
        _thisButton = GetComponent<Button>();

        _thisButton.onClick.AddListener(OpenSessionTable);
    }

    private void OnDisable()
    {
        _thisButton.onClick.RemoveListener(OpenSessionTable);
    }

    private void OpenSessionTable()
    {
        _buttonPanel.SetActive(false);
        _sessionTable.SetActive(true);
    }
}
