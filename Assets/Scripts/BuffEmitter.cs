using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffEmitter : MonoBehaviour
{
    [SerializeField] private Buff buff;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance.buffRecieverContainer.ContainsKey(collision.gameObject))
        {
            GameManager.Instance.buffRecieverContainer[collision.gameObject].AddBuff(buff);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GameManager.Instance.buffRecieverContainer.ContainsKey(collision.gameObject))
        {
            GameManager.Instance.buffRecieverContainer[collision.gameObject].RemoveBuff(buff);
        }
    }
}

[System.Serializable]
public class Buff
{
    public BuffType type;
    public float additiveBonus;
    public float multipleBonus;
}

public enum BuffType : byte
{
    Damage, Force, Armor
}