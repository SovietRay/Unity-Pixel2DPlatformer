using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicine : MonoBehaviour
{
    public int addHp = 50;
    private string playerTag = "Player";
    private string enemyTag = "Enemy";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag) || (collision.gameObject.CompareTag(enemyTag)))
        {
            Health health = collision.gameObject.GetComponent<Health>();
            health.SetHealth(addHp);
            Destroy(gameObject);
        }
    }
}
