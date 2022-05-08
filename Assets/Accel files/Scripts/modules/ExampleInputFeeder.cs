using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//remember to use this include
using AccelEngine;


public class ExampleInputFeeder : MonoBehaviour, IInputManager
{
    //variables required to be set by the Inputmanager interface
    public bool ActionKey1 { get; set; }
    public bool ActionKey2 { get; set; }
    public bool ActionKey3 { get; set; }
    public float Xinput1 { get; set; }
    public float Yinput1 { get; set; }
    public float Xinput2 { get; set; }
    public float Yinput2 { get; set; }

    void Update()
    {
        ActionKey1 = Input.GetButton("Fire1");
        ActionKey2 = Input.GetButton("Fire2");
        ActionKey3 = Input.GetButton("Fire3");
        Xinput1 = Input.GetAxis("Horizontal");
        Yinput1 = Input.GetAxis("Vertical");
        Xinput2 = Input.GetAxis("Debug Horizontal");
        Yinput2 = Input.GetAxis("Debug Vertical");
    }
}
