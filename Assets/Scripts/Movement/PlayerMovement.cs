using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ContactFilter2D CAN BE USED TO CHANGE PLAYER DASH COLLISIONS

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D col;
    private bool grounded = false;
    private Animator anim;


    [Header("Jumping Settings")]
    [Range(0.0f, 10.0f)] public float height = 2;
    private bool jumpedThisPress;
    [Tooltip("distance between jumping rays, to accomodate more X-wide characters")]
    public float RayOffset = 0.3f;

    [Header("Walking")]
    [SerializeField] private int speed = 10;
    [SerializeField] [Range(0f,5f)] private float turnPower = 2f;
    [SerializeField] private Vector2 desiredVelocity = new Vector3(0f, 0f, 0f);
    [SerializeField] private float maxAcceleration = 10;
    private float maxSpeedChange;
    [SerializeField] private float maxAirAcceleration = 50;
    private float maxAirSpeedChange;
    private Vector3 _velocity = new Vector2(0f, 0f);

    private void Update() => LiveUpdate();

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        col = this.GetComponent<CapsuleCollider2D>();

        maxSpeedChange = maxAcceleration * Time.fixedDeltaTime;
        maxAirSpeedChange = maxAirAcceleration * Time.fixedDeltaTime;

        anim = this.transform.GetChild(0).GetComponent<Animator>();
    }

    private void LiveUpdate()
    {
        anim.SetFloat("XVelocity", rb.velocity.x);
        anim.SetFloat("YVelocity", rb.velocity.y);
        desiredVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, 0.0f);
    }

    private void FixedUpdate()
    {
        ///////////
        /// Jumping
        ///////////
        if (Input.GetButton("Jump") && !jumpedThisPress)
        {
            for (int i = 0; i <= 2; i++)
            {
                //bounds returns a square that can cover the collider, not the actual colider variables
                //Debug.Log(col.bounds.size.x);

                //this doesn't work on default objects because their center is in their core. we need collider
                //to offset the math to the feet/base of object
                RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position - new Vector3(0.3f * (i - 1), 0f, 0f), Vector2.down, 0.1f);
                Debug.DrawRay(transform.position - new Vector3(0.3f * (i - 1), 0f, 0f), Vector2.down, Color.cyan);

                for (int j = 0; j < hit.Length; j++)
                {
                    if (hit[j].collider != null && hit[j].transform.tag != "NotJumpable")
                    {
                        Debug.Log("Jumped from" + hit[j].collider);

                        anim.Play("Jump");

                        Vector2 newVelocity = new Vector2(rb.velocity.x, Mathf.Sqrt(2f * Physics2D.gravity.magnitude * height));
                        rb.velocity = newVelocity;
                        jumpedThisPress = true;
                        break;
                    }
                }               
            }
        }
        else if (!Input.GetButton("Jump"))
        {
            jumpedThisPress = false;
        }

        ///////////
        /// Walking
        ///////////

        for (int i = 0; i <= 2; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position - new Vector3(0.3f * (i - 1), 0f, 0f), Vector2.down, 0.1f);
            if (hit.collider != null)
            {
                grounded = true;
            }
            else grounded = false;
        }

        
        if (rb.velocity.x < desiredVelocity.x)
        {
            if (grounded)
            {
                _velocity.x = Mathf.Min(_velocity.x + maxSpeedChange / 2, desiredVelocity.x);

                if (Mathf.Clamp(rb.velocity.x,-1f,1f) * Mathf.Clamp(desiredVelocity.x, -1f, 1f) <  0) //we are trying to turn around
                {
                    _velocity.x = Mathf.Min(_velocity.x + maxSpeedChange * turnPower, desiredVelocity.x);
                }
            }
            else
            {
                _velocity.x = Mathf.Min(_velocity.x + maxAirSpeedChange, desiredVelocity.x);
            }
        }
        else if (rb.velocity.x > desiredVelocity.x)
        {
            if (grounded)
            {
                _velocity.x = Mathf.Max(_velocity.x - maxSpeedChange / 2, desiredVelocity.x);

                if (Mathf.Clamp(rb.velocity.x, -1f, 1f) * Mathf.Clamp(desiredVelocity.x, -1f, 1f) < 0) //we are trying to turn around
                {
                    _velocity.x = Mathf.Max(_velocity.x - maxSpeedChange * turnPower, desiredVelocity.x);
                }
            }
            else
            {
                _velocity.x = Mathf.Max(_velocity.x - maxAirSpeedChange, desiredVelocity.x);
            }
        }
        
        rb.velocity = new Vector2(_velocity.x, rb.velocity.y);

        ///////////
        /// Walking
        ///////////
        

    }
}
