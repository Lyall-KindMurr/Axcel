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
    public float Delay;
    public float YVelocity;
    public float XVelocity;


    private Animator anim;
    private float timeBetweenAttacks;
    public float startAttack; // we need this to tell the difference of time between attacks?

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timeBetweenAttacks += Time.deltaTime;
        anim.SetFloat("Delay", timeBetweenAttacks - startAttack);

        if(Attack == true)
        {
            Attack = false;
            anim.SetTrigger("Attack");
        }
    }
}
