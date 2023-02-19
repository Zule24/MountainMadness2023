using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJump : MonoBehaviour
{
    public float jumpForce;
    public float jumpTime;
    private float jumpTimeCounter;
    private bool stoppedJumping;

    public bool grounded;
    private Rigidbody2D rb;
    private Animator myAnimator;

    [SerializeField] private Transform groundcheck;
    [SerializeField] private float radOCircle;
    [SerializeField] private LayerMask whatIsGround;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        jumpTimeCounter = jumpTime;
    }

    // Update is called once per frame
    private void Update()
    {
        //what it means to be grounded
        grounded = Physics2D.OverlapCircle(groundcheck.position, radOCircle, whatIsGround);

        if (grounded)
        {
            jumpTimeCounter = jumpTime;
            myAnimator.ResetTrigger("jump");
            myAnimator.SetBool("landing", false);
        }
        
        //if we press the jump button
        if (Input.GetButtonDown("Jump") && grounded)
        {
            //jump!!!
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            stoppedJumping = false;
            //jump animation
            myAnimator.SetTrigger("jump");
        }

        //if we hold the jump button
        if (Input.GetButton("Jump") && !stoppedJumping && (jumpTimeCounter > 0))
        {
            //jump!!!
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpTimeCounter -= Time.deltaTime;
            myAnimator.SetTrigger("jump");
        }

        //if we release the jump button
        if (Input.GetButtonUp("Jump"))
        {
            jumpTimeCounter = 0;
            stoppedJumping = true;
            myAnimator.SetBool("landing", true);
            myAnimator.ResetTrigger("jump");
        }
        if (rb.velocity.y < 0)
        {
            myAnimator.SetBool("landing", true);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundcheck.position,radOCircle);
    }
    private void FixedUpdate()
    {
        HandleLayers();
    }

    private void HandleLayers()
    {
        if (!grounded)
        {
            myAnimator.SetLayerWeight(1, 1);

        }
        else
        {
            myAnimator.SetLayerWeight(1, 0);
        }
    }
}
