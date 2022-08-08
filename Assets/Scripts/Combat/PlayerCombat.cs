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
        StartCoroutine("Attack");
    }

    //Cooldown time between attacks (in seconds)
    public float cooldown = 0.5f;
    public bool cancelable = false;
    //Max time before combo ends (in seconds)
    public float maxTime = 0.8f;
    //Max number of attacks in combo
    public int maxCombo = 5;
    //Current combo
    int combo = 0;
    //Time of last attack
    float lastTime;
    

    IEnumerator Attack()
    {
        //Constantly loops so you only have to call it once
        while (true)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                combo++;
                Debug.Log("Attack " + combo);
                lastTime = Time.time;
                playerAnim.AttackAnim(combo);

                //Combo loop that ends the combo if you reach the maxTime between attacks, or reach the end of the combo
                while ((Time.time - lastTime) < maxTime && combo < maxCombo)
                {
                    //Attacks if your cooldown has reset
                    if (Input.GetButtonDown("Fire1") && (Time.time - lastTime) > cooldown)
                    {
                        combo++;


                        Debug.Log("Attack " + combo); ///////////////////////// play animation state attack.
                        playerAnim.AttackAnim(combo);


                        lastTime = Time.time;
                    }
                    //Cancel the attack
                    if (cancelable == true)
                    {
                        break;
                    }
                    yield return null;
                }
                //Resets combo and waits the remaining amount of cooldown time before you can attack again to restart the combo
                combo = 0;
                yield return new WaitForSeconds(cooldown - (Time.time - lastTime));
            }
            yield return null;
        }
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

    /*
    private void Attack()
    {
        //sent to animator
        playerAnim.Attack = true;
        playerAnim.startAttack = Time.time;
        
        //check if weapon is hitscan
        //damage according to weapon
    }
    */
}
