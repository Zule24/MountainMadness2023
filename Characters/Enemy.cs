using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    
    public int currenthealth;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        hp = 100;
        currenthealth = hp;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
    protected override void HandleJumping()
    {

    }
    protected override void HandleAttack()
    {

    }
    public void TakeDamage(int damage)
    {
        currenthealth -= damage;
        myAnimator.SetTrigger("hit");
        if (currenthealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        myAnimator.SetBool("dead",true);
        //myAnimator.SetBool("dead", false);
        Debug.Log("Enemy died!");
        rb.gravityScale -= 1;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
