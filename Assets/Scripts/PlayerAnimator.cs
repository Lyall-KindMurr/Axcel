using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public bool Jumping;
    public bool Attack;
    public bool Rolling;
    public bool Grappled;
    public bool Hit;
    public bool Grounded;
    public float Delay;
    public float YVelocity;
    public float XVelocity;


    public CharacterState currentState = new IdleState();
    private Animator anim;
    private float timeBetweenAttacks;
    //public float startAttack; // we need this to tell the difference of time between attacks?

    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    public void AttackAnim (int counter)
    {
        /////play the counter's attack
        anim.Play("Attack " + counter.ToString());
        currentState = new AttackState();
    }

    // Update is called once per frame
    void Update()
    {
        XVelocity = anim.GetFloat("XVelocity");
        YVelocity = anim.GetFloat("YVelocity");
        // PRIORITIZE CURRENT ANIMATION BASED ON STATES

        if (currentState is IdleState)
        {
            //if grounded and X movement, 
            if(Grounded && Mathf.Abs(XVelocity) >=  0.1f)
            {
                anim.Play("Walk");
            }
            else if(Mathf.Abs(YVelocity) >= 0.1f)
            {
                currentState = new JumpingState();
            }
            else
            {
                anim.Play("Idle");
                //Debug.Log("IM IDLE");
            }
        }
        else if (currentState is AttackState)
        {
            float temp = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (temp > 1.0f)
                currentState = new IdleState();
            //Debug.Log("IM Attacking");
        }
        else if(currentState is JumpingState) // this acts more like an air movement handler.
        {
            if (YVelocity > 0.1f)
            {
                anim.Play("Jump");
            }
            else if (Grounded)
            {
                currentState = new IdleState();
            }
            else
            {
                anim.Play("Falling");
            }
        }

        /*
        timeBetweenAttacks += Time.deltaTime;
        anim.SetFloat("Delay", timeBetweenAttacks - startAttack);

        if(Attack == true)
        {
            Attack = false;
            anim.SetTrigger("Attack");
        }
        */
    }
}
