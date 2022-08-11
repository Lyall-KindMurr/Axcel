using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public int health;
    public PlayerMovement movementController;
    public bool canAttack = true;
    public PlayerAnimator playerAnim;


    // Start is called before the first frame update
    void Start()
    {
        health = 50;
        movementController = this.GetComponent<PlayerMovement>();
        playerAnim = this.transform.GetComponent<PlayerAnimator>();
        StartCoroutine("Attack");
    }

    public float cooldown = 0.5f;
    public bool cancelable = false;
    public float maxTime = 0.8f;
    public int maxCombo = 5;
    //Current combo
    int combo = 0;
    //Time of last attack
    float lastTime;
    

    IEnumerator Attack()
    {
        while (true)
        {
            if (Input.GetButtonDown("Fire1") && canAttack) //&& has weapon in that slot
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

    public void TakeDamage(int damage)
    {
        Debug.Log("Damage received on player, taking " + damage);
        health = health - damage;
        if(health < 0)
        {
            StartCoroutine("Waitseconds");
            Debug.Log("damn, you died, i should shut down now.");
            Application.Quit();
        }
    }

    IEnumerator Waitseconds()
    {
        yield return new WaitForSeconds(5);
    }

    void Update()
    {
        /*
        if (Input.GetButtonDown("Fire1") && canAttack) //&& has weapon in that slot
        {
            Attack();
        }
        if (Input.GetButtonDown("Fire2") && canAttack) //&& has weapon in that slot
        {
            Attack();
        }
        */
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
