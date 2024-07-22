using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIOpenSessionTabeButton : MonoBehaviour
{
    [SerializeField] private GameObject _createGamePanel;

    private Button _thisButton;

    private void OnEnable()
    {
        _thisButton = GetComponent<Button>();

        _thisButton.onClick.AddListener(OpenCreateGamePanel);
    }

    private void OpenCreateGamePanel()
    {
        _createGamePanel.SetActive(true);
    }
}
