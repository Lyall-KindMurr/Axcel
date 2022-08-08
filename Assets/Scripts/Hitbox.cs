using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    bool isenemy;
    int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SendMessage("TakeDamage", damage);
        Debug.Log("Enemy attacked");
    }
}
