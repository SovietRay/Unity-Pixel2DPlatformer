using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.healthContainer.Add(gameObject, this);
    }
    public int health;
    public void TakeHit (int damage)
    {
        health -= damage;
        Debug.Log(health);
        if (health <= 0)
            Destroy(gameObject);
    }
    public void SetHealth (int health)
    {
        this.health += health;
        Debug.Log(this.health);
    }

}
