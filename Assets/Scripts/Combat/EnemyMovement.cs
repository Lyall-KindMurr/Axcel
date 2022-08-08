using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D col;
    private bool isAttacking;


    [Header("Walking")]
    [SerializeField] private int speed = 4;
    [SerializeField] private Vector2 desiredVelocity = new Vector2(0f, 0f);
    [SerializeField] private float maxAcceleration = 10;
    private float maxSpeedChange;
    private Vector3 _velocity = new Vector2(0f, 0f);

    public float _side;

    [Header("AI")]
    public bool goingLeft;
    public Coroutine walk;
    bool isWalking = true;
    public GameObject hitbox;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        col = this.GetComponent<BoxCollider2D>();
        walk = StartCoroutine(CalculateDirection());

        maxSpeedChange = maxAcceleration * Time.fixedDeltaTime;
    }
    //desiredVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, 0.0f);

    // Update is called once per frame
    void FixedUpdate()
    {
        //we check in front, and if there's no platform, turn around
        _side = goingLeft ? 1f : -1f;

        if (goingLeft)
        {
            desiredVelocity.x = -1.0f * speed;
        }
        else
        {
            desiredVelocity.x = 1.0f * speed;
        }

        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position - new Vector3(_side, 0f, 0f), Vector2.down, col.size.y / 2 + 0.1f);
        Debug.DrawRay(transform.position - new Vector3(_side, -col.size.y / 2 + 0.1f, 0f), Vector2.down, Color.yellow);
        if (hit.Length == 0 && goingLeft)
        {
            desiredVelocity.x = 0f;
            TurnAround();
        }
        else if (hit.Length == 0 && !goingLeft)
        {
            desiredVelocity.x = 0f;
            TurnAround();
        }

        if (isWalking)
            Walk();

        
        hit = Physics2D.RaycastAll(transform.position, Vector2.left, 1.0f);
        Debug.DrawRay(transform.position, Vector2.left);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.tag == "Player" && !isAttacking)
            {
                isAttacking = true;
                isWalking = false;
                rb.velocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Static;
                StartCoroutine("WaitThenAttack");
                break;
            }
        }
        
    }

    IEnumerator CalculateDirection()
    {
        while (true)
        {
            if (rb.velocity.x < desiredVelocity.x)
            {
                if (Mathf.Clamp(rb.velocity.x, -1f, 1f) * Mathf.Clamp(desiredVelocity.x, -1f, 1f) < 0) //we are trying to turn around
                {
                    _velocity.x = Mathf.Min(_velocity.x + maxSpeedChange * 10, desiredVelocity.x);
                }
                _velocity.x = Mathf.Min(_velocity.x + maxSpeedChange, desiredVelocity.x);
            }
            else if (rb.velocity.x > desiredVelocity.x)
            {
                if (Mathf.Clamp(rb.velocity.x, -1f, 1f) * Mathf.Clamp(desiredVelocity.x, -1f, 1f) < 0) //we are trying to turn around
                {
                    _velocity.x = Mathf.Max(_velocity.x - maxSpeedChange * 10, desiredVelocity.x);
                }
                _velocity.x = Mathf.Max(_velocity.x - maxSpeedChange, desiredVelocity.x);
            }
            yield return null;
        }
    }

    void Walk()
    {
        rb.velocity = new Vector2(_velocity.x, rb.velocity.y);
    }

    IEnumerator WaitThenAttack()
    {
        yield return new WaitForSeconds(1);
        hitbox.SetActive(true);
        yield return null;
        yield return null;
        yield return null;
        hitbox.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
        isWalking = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    void TurnAround()
    {
        if (goingLeft)
            goingLeft = false;
        else if (!goingLeft)
            goingLeft = true;
    }
}
