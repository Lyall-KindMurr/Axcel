using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyDamageManager : MonoBehaviour
{
    public PlayerCombat main;

    private void Awake()
    {
        transform.parent.GetComponent<PlayerCombat>();
    }

    void TakeDamage(int damage)
    {
        Debug.Log("Taken damge on player, sending data to combat controller");
        main.TakeDamage(damage);
    }
}
