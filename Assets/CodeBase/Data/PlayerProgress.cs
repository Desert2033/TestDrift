using System;

[Serializable]
public class PlayerProgress
{
    private const int GoldDebugVersion = 100;
    private const int CashDebugVersion = 1500;

    public CarGarageData CarGarageData;
    public WalletData WalletData;

    public PlayerProgress()
    {
        CarGarageData = new CarGarageData();
        WalletData = new WalletData(GoldDebugVersion, CashDebugVersion);
    }
}