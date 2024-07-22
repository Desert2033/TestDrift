using UnityEngine;
using TMPro;
using Zenject;

public class UITimeOverPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeText;
    
    private ITimeOverService _timeOverService;

    [Inject]
    public void Construct(ITimeOverService timeOverService)
    {
        _timeOverService = timeOverService;
    }

    private void OnEnable()
    {
        _timeOverService.OnTimeOverChange += TimeChange;
    }

    private void OnDisable()
    {
        _timeOverService.OnTimeOverChange -= TimeChange;
    }

    private void TimeChange(float time)
    {
        float minutes = time / 60;
        float seconds = time % 60  >= 0 ? time % 60 : 0;

        string strMinutes = (int)minutes > 9 ? $"{(int)minutes}" : $"0{(int)minutes}";
        string strSeconds = (int)seconds > 9 ? $"{(int)seconds}" : $"0{(int)seconds}";

        _timeText.text = $"TimeOver: {strMinutes}:{strSeconds}";
    }

}
