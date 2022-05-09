using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AccelEngine
{
    public class Walking : MonoBehaviour, IMovementModifier
    {
        [SerializeField]
        private AccelCore accelCore = null;

        public bool Absolute { get; private set; }
        public bool Priority { get; private set; }
        public Vector3 Value { get; private set; }

        [Header("Settings")]
        [SerializeField] private int speed = 5;
        [SerializeField] private Vector3 desiredVelocity = new Vector3(0f, 0f, 0f);
        [SerializeField] private float maxAcceleration = 1;
        private Rigidbody2D velocity;
        private Vector3 _velocity = new Vector2(0f, 0f);

        //////////////////////////////////////
        // DO NOT REMOVE THE ONENABLE AND ONDISABLE
        //////////////////////////////////////
        private void OnEnable()
        {
            Priority = false;
            Absolute = false;
            accelCore = GetComponent<AccelCore>();
            accelCore.AddModule(this);
            velocity = GetComponent<Rigidbody2D>();
        }
        private void OnDisable()
        {
            accelCore = GetComponent<AccelCore>();
            accelCore.RemoveModule(this);
        }

        private void Update()
        {
            desiredVelocity = new Vector2(accelCore.Xinput1 * speed, 0.0f);
        }

        private void FixedUpdate()
        {
            _velocity = velocity.velocity;
            float maxSpeedChange = maxAcceleration * Time.fixedDeltaTime;
            Debug.Log(_velocity);
            Debug.Log(maxSpeedChange);
            //velocity added based on input

            //<<<<<<------X------>>>>>>

            if (_velocity.x < desiredVelocity.x)
            {
                _velocity.x = Mathf.Min(_velocity.x + maxSpeedChange, desiredVelocity.x);
            }
            else if (_velocity.x > desiredVelocity.x)
            {
                _velocity.x = Mathf.Max(_velocity.x - maxSpeedChange, desiredVelocity.x);
            }
            velocity.velocity = _velocity;
        }
    }
}
