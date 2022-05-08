using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AccelEngine
{
    public class Walking : MonoBehaviour, IMovementModifier
    {
        [SerializeField]
        private AccelCore axcelCore = null;

        public bool Absolute { get; private set; }
        public bool Priority { get; private set; }
        public Vector3 Value { get; private set; }

        [Header("Settings")]
        private int speed = 5;

        //////////////////////////////////////
        // DO NOT REMOVE THE ONENABLE AND ONDISABLE
        //////////////////////////////////////
        private void OnEnable()
        {
            Priority = false;
            Absolute = false;
            axcelCore = GetComponent<AccelCore>();
            axcelCore.AddModule(this);
        }
        private void OnDisable()
        {
            axcelCore = GetComponent<AccelCore>();
            axcelCore.RemoveModule(this);
        }


        //move this to jumping, dunce
        private void FixedUpdate()
        {
            /*
            Vector2 PlayerInput;
            PlayerInput.x = Input.GetAxis("Horizontal");
            PlayerInput.y = Input.GetAxis("Vertical");

            //we get a smoother output if the desired speed is calculated on every displayed frame
            desiredVelocity = new Vector2(PlayerInput.x, PlayerInput.y) * MaxSpeed;

            if (Input.GetKeyDown("k"))
            {
                rb.AddForce(Vector2.up * Mathf.Sqrt(2 * Physics2D.gravity.magnitude * 2f), ForceMode2D.Impulse);
                Debug.Log("jumped");
            }
            */
            if (Input.GetKeyDown("k"))
            {
                float height = 2.0f;
                Value = Vector2.up * Mathf.Sqrt(Physics2D.gravity.magnitude * height);
            }
            else
            {
                Value = Vector2.zero;
            }
        }
    }
}
