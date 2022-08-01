using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D col;


    [Header("Jumping Settings")]
    [Range(0.0f, 10.0f)] public float height = 2;
    private bool jumpedThisPress;
    [Tooltip("distance between jumping rays, to accomodate more X-wide characters")]
    public float RayOffset = 0.3f;

    [Header("Please show up Walking")]
    public int speeds;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();

        col = this.GetComponent<CapsuleCollider2D>();
    }

    private void FixedUpdate()
    {
        ///////////
        /// Jumping
        ///////////
        if (Input.GetButton("Fire1") && !jumpedThisPress)
        {
            for (int i = 0; i <= 2; i++)
            {
                //bounds returns a square that can cover the collider, not the actual colider variables
                //Debug.Log(col.bounds.size.x);

                //this doesn't work on default objects because their center is in their core. we need collider
                //to offset the math to the feet/base of object
                RaycastHit2D hit = Physics2D.Raycast(transform.position - new Vector3(0.3f * (i - 1), 0f, 0f), Vector2.down, 0.1f);
                Debug.DrawRay(transform.position - new Vector3(-0.5f + 0.3f * i, 0f, 0f), Vector2.down, Color.red);
                Debug.Log(hit.collider);
                if (hit.collider != null && hit.transform.tag != "NotJumpable")
                {
                    Debug.Log(hit.collider);

                    Vector2 newVelocity = new Vector2(rb.velocity.x, Mathf.Sqrt(2f * Physics2D.gravity.magnitude * height));
                    rb.velocity = newVelocity;
                    jumpedThisPress = true;
                    break;
                }
            }
        }
        else if (!Input.GetButton("Fire1"))
        {
            jumpedThisPress = false;
        }

        ///////////
        /// Walking
        ///////////
    }
}
