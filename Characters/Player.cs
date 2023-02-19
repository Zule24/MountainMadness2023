using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public float runSpeed = 2.0f;
    //private float walkSpeed = 1.0f;
    public Transform Hitbox;
    [SerializeField] public float attackRange = 0.5f;
    public LayerMask Enemies;
    private float nextAttackTime = 0f;
    public override void Start()
    {
        base.Start();
        speed = runSpeed;
        attackvalue = 40;
        attackrate = 2f;
    }

    public override void Update()
    {
        base.Update();
        direction = Input.GetAxisRaw("Horizontal");
        HandleJumping();
        if (Time.time >= nextAttackTime)
        {
            HandleAttack();
            
        }
    }
    protected override void HandleMovement()
    {
        base.HandleMovement();
        myAnimator.SetFloat("speed", Mathf.Abs(direction));
        TurnAround(direction);
    }
    protected override void HandleJumping()
    {
        if (grounded)
        {
            jumpTimeCounter = jumpTime;
            myAnimator.ResetTrigger("jump");
            myAnimator.SetTrigger("stopattack");
            myAnimator.SetBool("landing", false);
        }

        //if we press the jump button
        if (Input.GetButtonDown("Jump") && grounded)
        {
            //jump!!!
            Jump();
            stoppedJumping = false;
            //jump animation
            myAnimator.SetTrigger("jump");
        }

        //if we hold the jump button
        if (Input.GetButton("Jump") && !stoppedJumping && (jumpTimeCounter > 0))
        {
            //jump!!!
            Jump();
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
        if (Input.GetButtonDown("Fire1") && !grounded)
        {
            //attack!
            myAnimator.SetTrigger("click");

        }
    }
    protected override void HandleAttack()
    {
        if (Input.GetButtonDown("Fire1") && grounded)
        {
            //attack!
            myAnimator.SetTrigger("click");
            nextAttackTime = Time.time + 1f / attackrate;
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(Hitbox.position, attackRange, Enemies);
            foreach(Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackvalue);
                Debug.Log("We hit " + enemy.name);
                
            }
            
        }
    }
    void OnDrawGizmosSelected()
    {
        if (Hitbox == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(Hitbox.position, attackRange);
    }
}
