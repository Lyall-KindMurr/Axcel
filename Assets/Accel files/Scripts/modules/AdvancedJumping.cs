using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AccelEngine;

public class AdvancedJumping : MonoBehaviour, IMovementModifier
{
    [SerializeField]
    private AccelCore accelCore = null;

    public bool Absolute { get; private set; }
    public bool Priority { get; private set; }
    public Vector3 Value { get; private set; }

    [Header("Settings")]
    [Range(0.0f, 10.0f)] public float height = 2;
    private bool jumpedThisPress;

    //////////////////////////////////////
    // DO NOT REMOVE THE ONENABLE AND ONDISABLE
    //////////////////////////////////////
    private void OnEnable()
    {
        Priority = false;
        Absolute = false;
        accelCore = GetComponent<AccelCore>();
        accelCore.AddModule(this);
    }
    private void OnDisable()
    {
        accelCore = GetComponent<AccelCore>();
        accelCore.RemoveModule(this);
    }

    private void FixedUpdate()
    {
        if (accelCore.ActionKey1 && !jumpedThisPress)
        {
            for (int i = 0; i <= 2; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position - new Vector3(-0.5f + 0.3f*i,0f,0f), Vector2.down, 0.1f);
                if (hit.collider != null && hit.transform.tag != "NotJumpable")
                {
                    Value = Vector2.up * Mathf.Sqrt(2f * Physics2D.gravity.magnitude * height);
                    jumpedThisPress = true;
                    break;
                }
            }           
        }
        else if (!accelCore.ActionKey1)
        {
            Value = Vector2.zero;
            jumpedThisPress = false;
        }
        else
        {
            Value = Vector2.zero;
        }
    }
}
