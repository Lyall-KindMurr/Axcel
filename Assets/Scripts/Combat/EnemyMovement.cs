using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D col;

    [Header("Walking")]
    [SerializeField] private int speed = 4;
    [SerializeField] private Vector2 desiredVelocity = new Vector2(0f, 0f);
    [SerializeField] private float maxAcceleration = 10;
    private float maxSpeedChange;
    [SerializeField] private float maxAirAcceleration = 50;
    private float maxAirSpeedChange;
    private Vector3 _velocity = new Vector2(0f, 0f);

    public float _side;

    [Header("AI")]
    public bool goingLeft;


    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        col = this.GetComponent<BoxCollider2D>();

        maxSpeedChange = maxAcceleration * Time.fixedDeltaTime;
    }
    //desiredVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, 0.0f);

    // Update is called once per frame
    void FixedUpdate()
    {
        //we check in front, and if there's no platform, turn around
        _side = goingLeft ? 1f : -1f;

        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position - new Vector3(_side, 0f, 0f), Vector2.down, col.size.y / 2 + 0.1f);
        Debug.DrawRay(transform.position - new Vector3(_side, -col.size.y / 2 + 0.1f, 0f), Vector2.down, Color.yellow);
        if (hit.Length == 0 && goingLeft)
        {
            Debug.Log("Turning around");
            TurnAround();
        }
        else if (hit.Length == 0 && !goingLeft)
        {
            Debug.Log("Turning around");
            TurnAround();
        }

        if (goingLeft)
        {
            desiredVelocity.x = -1.0f * speed;
        }
        else
        {
            desiredVelocity.x = 1.0f * speed;
        }


        //copied from the player controller
        if (rb.velocity.x < desiredVelocity.x)
        {
            _velocity.x = Mathf.Min(_velocity.x + maxSpeedChange, desiredVelocity.x);
        }
        else if (rb.velocity.x > desiredVelocity.x)
        {
            _velocity.x = Mathf.Max(_velocity.x - maxSpeedChange, desiredVelocity.x);
        }
        rb.velocity = new Vector2(_velocity.x, rb.velocity.y);
    }

    void TurnAround()
    {
        if (goingLeft)
            goingLeft = false;
        else if (!goingLeft)
            goingLeft = true;
    }
}
