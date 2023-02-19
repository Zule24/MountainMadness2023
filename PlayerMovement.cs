using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    //necessary for animations and physics
    private Rigidbody2D rb2D;
    private Animator myAnimator;
    private bool facingRight = true;

    public float speed = 2.5f;
    public float horizMovement; //-1 or 0 or 1

    // Start is called before the first frame update
    private void Start()
    {
        //define gameobjects
        rb2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

    }

    // Update is called once per frame
    private void Update()
    {
        //check if player has an input
        horizMovement = Input.GetAxisRaw("Horizontal");
    }
    private void FixedUpdate()
    {
        //move the character
        rb2D.velocity = new Vector2(horizMovement * speed, rb2D.velocity.y);
        myAnimator.SetFloat("speed", Mathf.Abs(horizMovement));
        Flip(horizMovement);
    }
    private void Flip(float horizontal)
    {
        //if facing left && moving left
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
