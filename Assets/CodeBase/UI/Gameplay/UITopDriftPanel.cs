using UnityEngine;
using TMPro;
using System;
using Zenject;

public class UITopDriftPanel : MonoBehaviour
{
    private const int MaxAngle = 120;
    private const float MinimumSpeed = 5f;
    private const float MinimumAngle = 10f;

    [SerializeField] private GameObject _driftscorePanel;
    [SerializeField] private TextMeshProUGUI _totalScoreText;
    [SerializeField] private TextMeshProUGUI _driftScoreText;

    private float _speed = 0;
    private float _driftAngle = 0;
    private float _currentDriftScore;
    private IDriftScoreService _driftScoreService;

    [NonSerialized] public Rigidbody DriftTarget;

    public static UITopDriftPanel Instance;

    [Inject]
    public void Construct(IDriftScoreService driftScoreService)
    {
        _driftScoreService = driftScoreService;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (DriftTarget != null)
        {
            UpdateDrift();
            UpdateUI();
        }
    }

    private void UpdateDrift()
    {
        if (CanDrifting())
        {
            StartDrift();
        }
        else
        {
            StopDrift();
        }
    }

    private bool CanDrifting()
    {
        _speed = DriftTarget.velocity.magnitude;
        _driftAngle = Vector3.Angle(DriftTarget.transform.forward, (DriftTarget.velocity + DriftTarget.transform.forward).normalized);

        if (_driftAngle < MaxAngle && _driftAngle >= MinimumAngle && _speed > MinimumSpeed)
        {
            return true;
        }

        return false;
    }

    private void StartDrift()
    {
        _currentDriftScore += Time.deltaTime * _driftAngle;
        _driftscorePanel.SetActive(true);
    }

    private void StopDrift()
    {
        _driftScoreService.AddTotalScore(_currentDriftScore);
        _currentDriftScore = 0;

        _driftscorePanel.SetActive(false);
    }

    private void UpdateUI()
    {
        _totalScoreText.text = $"{(int)_driftScoreService.TotalScore}";
        _driftScoreText.text = $"{(int)_currentDriftScore}";
    }
}
