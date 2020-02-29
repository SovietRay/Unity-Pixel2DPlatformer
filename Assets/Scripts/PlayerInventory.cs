using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int coinsCount;
    public static PlayerInventory Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    public void AddCoin(int quantity)
    {
        coinsCount += quantity;
        Debug.Log($"Toss a coin to your witcher - {coinsCount}");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance.coinContainer.ContainsKey(collision.gameObject))
        {
            AddCoin(1);
            GameManager.Instance.coinContainer[collision.gameObject].StartDestroy();
        }
    }
}


