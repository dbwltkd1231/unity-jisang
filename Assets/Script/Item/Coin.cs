using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public int CoinMag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager.Instance.GetCoin(CoinMag);
            MonsterManager.Instance.RecevieCoin(this.gameObject);
        }
    }
}
