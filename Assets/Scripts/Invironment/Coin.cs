using UnityEngine;

public class Coin : MonoBehaviour
{
    public void Collect() => this.gameObject.SetActive(false);
}
