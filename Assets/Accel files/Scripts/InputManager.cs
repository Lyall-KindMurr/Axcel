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
    [SerializeField] Rect AllowedArea = new Rect(-5f, -5f, 10f, 10f);
    [SerializeField, Range(0f, 1f)] float Bounciness = 1f;

    [SerializeField] private Vector3 velocity = new Vector2(0f, 0f);

    void Start()
    {
        if (this.KeepSceneViewActive && Application.isEditor)
        {
            UnityEditor.SceneView.FocusWindowIfItsOpen(typeof(UnityEditor.SceneView));
        }
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

        // movement without limits
        Vector3 desiredVelocity = new Vector2(PlayerInput.x, PlayerInput.y) * MaxSpeed;
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

        if (velocity.y < desiredVelocity.y)
        {
            velocity.y = Mathf.Min(velocity.y + maxSpeedChange, desiredVelocity.y);
        }
        else if (velocity.y > desiredVelocity.y)
        {
            velocity.y = Mathf.Max(velocity.y - maxSpeedChange, desiredVelocity.y);
        }

        //displacement must be a vector3 to be addable to the transform without creating a new vector.
        Vector3 displacement = velocity * Time.deltaTime;

        //this creates our final vector, but we will apply it after collision checks
        Vector2 NewPosition = transform.localPosition + displacement;

        //Testing movement on collision, by creating a fake collider.

        if (CollisionTest)
        {
            if (NewPosition.x < AllowedArea.xMin)
            {
                NewPosition.x = AllowedArea.xMin;
                velocity.x = -velocity.x * Bounciness;
            }
            else if (NewPosition.x > AllowedArea.xMax)
            {
                NewPosition.x = AllowedArea.xMax;
                velocity.x = -velocity.x * Bounciness;
            }
            if (NewPosition.y < AllowedArea.yMin)
            {
                NewPosition.y = AllowedArea.yMin;
                velocity.y = -velocity.y * Bounciness;
            }
            else if (NewPosition.y > AllowedArea.yMax)
            {
                NewPosition.y = AllowedArea.yMax;
                velocity.y = -velocity.y * Bounciness;
            }

            Debug.DrawLine(new Vector3(AllowedArea.x, AllowedArea.y), new Vector3(AllowedArea.x + AllowedArea.width, AllowedArea.y), Color.green);
            Debug.DrawLine(new Vector3(AllowedArea.x, AllowedArea.y), new Vector3(AllowedArea.x, AllowedArea.y + AllowedArea.height), Color.red);
            Debug.DrawLine(new Vector3(AllowedArea.x + AllowedArea.width, AllowedArea.y + AllowedArea.height), new Vector3(AllowedArea.x + AllowedArea.width, AllowedArea.y), Color.green);
            Debug.DrawLine(new Vector3(AllowedArea.x + AllowedArea.width, AllowedArea.y + AllowedArea.height), new Vector3(AllowedArea.x, AllowedArea.y + AllowedArea.height), Color.red);
        }

        transform.localPosition = NewPosition;
    }
}
