using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public GameObject leftBorder;
    public GameObject rightBorder;
    public Rigidbody2D rigidbody;
    public GroundDetection groundDetection;
    public Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CollisionDmg collisionDmg;
    [SerializeField] private float speed = 5f;
    public float Speed
    {
        get { return speed; }
        set
        {
            if ((value > 0.5) && (value < 100))
                speed = value;
        }
    }
    public bool isRightDirection;

    private void FixedUpdate()
    {
        if (groundDetection.isGrounded)
        {
            if (transform.position.x > rightBorder.transform.position.x
                || collisionDmg.Direction < 0)
                isRightDirection = false;
            else if (transform.position.x < leftBorder.transform.position.x
                || collisionDmg.Direction > 0)
                isRightDirection = true;

            rigidbody.velocity = isRightDirection ? Vector2.right : Vector2.left;
            rigidbody.velocity *= speed;
        }

        //if (animator != null)
         //   animator.SetFloat("Speed", Mathf.Abs(rigidbody.velocity.x));

        if (rigidbody.velocity.x > 0)
            spriteRenderer.flipX = true;
        if (rigidbody.velocity.x < 0)
            spriteRenderer.flipX = false;
    }

    #region DeprecatedImplementation
    /*
    public GameObject leftBorder;
    public GameObject rightBorder;
    public bool isRightDirection;
    public Rigidbody2D rigidbody;
    public float speed;
    GroundDetection groundDetection;

    private Vector2 direction;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    private string collisionTag = "Player";
    private bool rightSide;
    private bool attacking;

    private void Start()
    {
        groundDetection = GetComponent<GroundDetection>();
    }
    private void Update()
    {
        if (!attacking)
            Walking();
            
        else
        {
            direction = Vector2.zero;
            rigidbody.velocity = direction;
            if (spriteRenderer != null)
            {
                if (rightSide)
                {
                    isRightDirection = spriteRenderer.flipX;
                    spriteRenderer.flipX = false;
                }
                else
                {
                    isRightDirection = spriteRenderer.flipX;
                    spriteRenderer.flipX = true;
                }
                    
            }

            //Debug.Log("iam stop");
        }
    }

    private void Walking()
    {
        if (animator != null)
            animator.SetFloat("Speed", Mathf.Abs(direction.x));

        direction = Vector2.zero;

        if (isRightDirection && groundDetection.isGrounded)
        {
            direction = Vector2.right;
            if (transform.position.x > rightBorder.transform.position.x)
            {
                isRightDirection = !isRightDirection;
                if (spriteRenderer != null)
                    spriteRenderer.flipX = false;
            }
        }
        else if (groundDetection.isGrounded)
        {
            direction = Vector2.left;
            if (transform.position.x < leftBorder.transform.position.x)
            {
                isRightDirection = true;
                if (spriteRenderer != null)
                    spriteRenderer.flipX = true;
            }
        }
        direction *= speed;
        direction.y = rigidbody.velocity.y;
        rigidbody.velocity = direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(collisionTag))
        {
            animator.SetTrigger("Attacking");
            attacking = true;
            if (gameObject.transform.position.x > collision.gameObject.transform.position.x)
                rightSide = true;
            else
                rightSide = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(collisionTag))
        {
            animator.SetTrigger("StopAttacking");
            attacking = false;
        }
    }
    */
    #endregion
}
