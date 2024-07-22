using TMPro;
using UnityEngine;
using Zenject;

public class UICashPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _cashText;

    private IWalletService _walletService;

    [Inject]
    public void Construct(IWalletService walletService)
    {
        _walletService = walletService;
    }

    private void OnEnable()
    {
        _walletService.OnChangeCash += ChangeValue;

        ChangeValue(_walletService.Cash);
    }

    private void OnDisable()
    {
        _walletService.OnChangeCash -= ChangeValue;
    }

    private void ChangeValue(float value)
    {
        _cashText.text = $"{value}";
    }
}
