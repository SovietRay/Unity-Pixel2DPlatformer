using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDmg : MonoBehaviour
{
    [SerializeField] Animator animator;
    private int damage = 10;

    public float Direction { get; private set; }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (GameManager.Instance.healthContainer.ContainsKey(collision.gameObject))
        {
            //animator.SetTrigger("CollisionDmg");
            Direction = (collision.transform.position - transform.position).x;
            animator.SetFloat("Direction", Mathf.Abs(Direction));
        }
    }

    private void SetDmg()
    {
        if (GameManager.Instance.healthContainer.ContainsKey(gameObject))
            GameManager.Instance.healthContainer[gameObject].TakeHit(damage);
        Direction = 0;
        animator.SetFloat("Direction", 0f);
    }
}
