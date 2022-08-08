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


    public CharacterState currentState;
    private Animator anim;
    private float timeBetweenAttacks;
    //public float startAttack; // we need this to tell the difference of time between attacks?

    // Start is called before the first frame update
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
            anim.Play("Idle");
            //Debug.Log("IM IDLE");

            //if grounded and X movement, 
            if(Grounded && Mathf.Abs(YVelocity) >=  0.1f) //nothign is setting Xvelocity
            {
                anim.Play("Jump");
                Debug.Log("Jumping");
            }
        }
        else if (currentState is AttackState)
        {
            float temp = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (temp > 1.0f)
                currentState = new IdleState();
            //Debug.Log("IM Attacking");
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
