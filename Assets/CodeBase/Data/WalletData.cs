using System;

[Serializable]
public class WalletData
{
    public float Gold;
    public float Cash;

    public WalletData(float gold, float cash)
    {
        Gold = gold;
        Cash = cash;
    }
}