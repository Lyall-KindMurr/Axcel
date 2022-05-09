using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : MonoBehaviour
{
    [Header("Basic movement handling", order = 1)]
    [Space(10)]

    [SerializeField] private bool KeepSceneViewActive;
    [SerializeField] private bool AbsoluteMovement;
    [SerializeField, Range(0f, 100f)] float MaxSpeed = 10f;
    [SerializeField, Range(0f, 100f)] float MaxAcceleration = 10f;
    [SerializeField] private bool CollisionTest;

    //variables that will not be displayed, but can be returned by method
    [SerializeField] private Vector3 velocity = new Vector2(0f, 0f);
    [SerializeField] private Vector3 desiredVelocity = new Vector3(0f, 0f, 0f);
    Rigidbody2D rb;

    [Header("Basic movement handling", order = 2)]
    [Space(10)]
    [SerializeField] private bool KeepSceneViewActive2;//testing spacing, lol

    private void Awake()
    {
        if (this.KeepSceneViewActive && Application.isEditor)
        {
            UnityEditor.SceneView.FocusWindowIfItsOpen(typeof(UnityEditor.SceneView));
        }

        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 PlayerInput;
        PlayerInput.x = Input.GetAxis("Horizontal");
        PlayerInput.y = Input.GetAxis("Vertical");

        //allow the user to use only max values of the axis
        if (AbsoluteMovement)
        {
            PlayerInput.Normalize();
        }
        else
        {
            PlayerInput = Vector2.ClampMagnitude(PlayerInput, 1f);
        }

        //we get a smoother output if the desired speed is calculated on every displayed frame
        desiredVelocity = new Vector2(PlayerInput.x, PlayerInput.y) * MaxSpeed;

        if (Input.GetKeyDown("k"))
        {
            rb.AddForce(Vector2.up * Mathf.Sqrt(2 * Physics2D.gravity.magnitude * 2f), ForceMode2D.Impulse);
            Debug.Log("jumped");
        }
    }

    private void FixedUpdate()
    {
        //This math works better in FixedUpdate due to syncing with the physics simulation

        velocity = rb.velocity;
        float maxSpeedChange = MaxAcceleration * Time.deltaTime;

        //velocity added based on input

        //<<<<<<------X------>>>>>>

        if (velocity.x < desiredVelocity.x)
        {
            velocity.x = Mathf.Min(velocity.x + maxSpeedChange, desiredVelocity.x);
        }
        else if (velocity.x > desiredVelocity.x)
        {
            velocity.x = Mathf.Max(velocity.x - maxSpeedChange, desiredVelocity.x);
        }

        //<<<<<<------Y------>>>>>>

        /*
        if (velocity.y < desiredVelocity.y)
        {
            velocity.y = Mathf.Min(velocity.y + maxSpeedChange, desiredVelocity.y);
        }
        else if (velocity.y > desiredVelocity.y)
        {
            velocity.y = Mathf.Max(velocity.y - maxSpeedChange, desiredVelocity.y);
        }
        */
        rb.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("hit something " + collision.collider.name); //othercollider returns this items name oddly
    }
}
