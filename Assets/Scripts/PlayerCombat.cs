using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public PlayerMovement movementController;
    public bool canAttack = true;
    public PlayerAnimator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        movementController = this.GetComponent<PlayerMovement>();
        playerAnim = this.transform.GetChild(0).GetComponent<PlayerAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canAttack) //&& has weapon in that slot
        {
            Attack();
        }
        if (Input.GetButtonDown("Fire2") && canAttack) //&& has weapon in that slot
        {
            Attack();
        }
        // weapon pickup? button and on a collider tagged as player? or something maybe new tag called pickupable
    }

    private void Attack()
    {
        //sent to animator
        playerAnim.Attack = true;
        playerAnim.startAttack = Time.time;
        
        //check if weapon is hitscan
        //damage according to weapon
    }
}
