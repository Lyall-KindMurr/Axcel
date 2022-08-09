using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public GameObject projectilePrefab;

    private void Awake()
    {
        Instantiate(projectilePrefab);
    }
}
