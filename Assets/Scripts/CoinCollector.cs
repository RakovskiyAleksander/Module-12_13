using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private Wallet _wallet;

    public void Initialize(Wallet wallet)
    {
        _wallet = wallet;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Coin>(out Coin coinBehaviour))
        {
            _wallet.AddCoin();
            coinBehaviour.Collect();
        }
    }
}
