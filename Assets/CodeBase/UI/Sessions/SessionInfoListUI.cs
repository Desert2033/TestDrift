using UnityEngine;
using Fusion;
using UnityEngine.UI;
using Zenject;

public class SessionInfoListUI : MonoBehaviour
{
    [SerializeField] private VerticalLayoutGroup _verticalLayoutGroup;
    
    private IUIFactory _uiFactory;

    [Inject]
    public void Construct(IUIFactory uiFactory)
    {
        _uiFactory = uiFactory;
    }

    public void ClearList()
    {
        foreach (Transform item in _verticalLayoutGroup.transform)
        {
            Destroy(item.gameObject);
        }
    }

    public void AddItem(SessionInfo sessionInfo) 
    {
        SessionItemInfoUI sessionItem = _uiFactory.CreateSessionItem(_verticalLayoutGroup.transform);

        sessionItem.SetInformation(sessionInfo);
    }
}
