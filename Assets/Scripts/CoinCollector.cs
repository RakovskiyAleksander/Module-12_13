public class CoinCollector
{
    private int _maxCoinsAmount;
    private int _coinsAmount;

    public bool IsFull { get; private set; }
    public int CoinsLeft => (_maxCoinsAmount - _coinsAmount);

    public CoinCollector(int maxCoinAmount)
    {
        _maxCoinsAmount = maxCoinAmount;
        _coinsAmount = 0;
        IsFull = false;
    }

    public void AddCoin()
    {
        _coinsAmount++;

        if (_coinsAmount == _maxCoinsAmount) IsFull = true;
    }
}
