using UnityEngine;
using Zenject;

public class UIGamePlayCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _driftPanel1;
    [SerializeField] private GameObject _driftPanel2;
    [SerializeField] private GameObject _finishPanel;

    private IEndGameService _endGameService;

    [Inject]
    public void Construct(IEndGameService endGameService)
    {
        _endGameService = endGameService;
    }

    private void Start()
    {
        _endGameService.OnEndGame += EndGame;
    }

    private void OnDisable()
    {
        _endGameService.OnEndGame -= EndGame;
    }

    private void EndGame()
    {
        _driftPanel1.SetActive(false);
        _driftPanel2.SetActive(false);
        _finishPanel.SetActive(true);
    }
}
