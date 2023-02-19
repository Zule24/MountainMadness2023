using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Character : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Movement")]
    [SerializeField] protected float speed = 1.0f;
    [SerializeField] protected float direction;
    protected bool facingRight = true;
    [Header("Jump Variables")]
    [SerializeField] protected float jumpForce;
    [SerializeField] protected float jumpTime;
    protected float jumpTimeCounter;
    protected bool stoppedJumping;
    [SerializeField] protected Transform groundcheck;
    [SerializeField] protected float radOCircle;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected bool grounded;

    [Header("Attack Variables")]
    [SerializeField] protected int attackvalue;
    [SerializeField] protected float attackrate;
    [Header("Character Stats")]
    public int hp =100;
    

    protected Rigidbody2D rb;
    protected Animator myAnimator;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        jumpTimeCounter = jumpTime;
    }
    public virtual void Update()
    {
        
        grounded = Physics2D.OverlapCircle(groundcheck.position, radOCircle, whatIsGround);
        if (rb.velocity.y < 0)
        {
            myAnimator.SetBool("landing", true);
        }
    }
    
    public virtual void FixedUpdate()
    {
        HandleMovement();
        HandleLayers();
    }
    protected void Move()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }
    protected void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    protected abstract void HandleJumping();
    protected abstract void HandleAttack();
    protected virtual void HandleMovement()
    {
        Move();
    }
    
    protected void TurnAround(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);

            //Vector3 theScale = transform.localScale;
            //theScale.x *= -1;
            //transform.localScale = theScale;
        }
    } 
    protected void HandleLayers()
    {
        if(!grounded)
        {
            myAnimator.SetLayerWeight(1, 1);

        }
        else
        {
            myAnimator.SetLayerWeight(1, 0);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundcheck.position, radOCircle);
    }
    
}
