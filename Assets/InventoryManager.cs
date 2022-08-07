using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject firstWeapon;
    private string typeFirst;
    public GameObject secondWeapon;
    private string typeSecond;

    void Awake()
    {
        CheckType();
        //this script might not be needed
    }

    private void CheckType()
    {
        if (transform.GetChild(0).tag == "Ranged")
        {
            typeFirst = "ranged";
        }
        else if (transform.GetChild(0).tag == "Melee")
        {
            typeFirst = "melee";
        }
        if (transform.GetChild(1).tag == "Ranged")
        {
            typeSecond = "ranged";
        }
        else if (transform.GetChild(1).tag == "Melee")
        {
            typeSecond = "melee";
        }
    }

    private void CheckType(int index)
    {

    }
}
