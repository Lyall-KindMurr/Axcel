using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AccelEngine
{
    public class Walking : MonoBehaviour, IMovementModifier
    {
        [SerializeField]
        private AxcelCore axcelCore = null;

        public Vector3 Value { get; private set; }
        public Vector3 tester;

        [Header("Settings")]
        private int speed = 5;

        private void OnEnable()
        {
            axcelCore = GetComponent<AxcelCore>();
            axcelCore.AddModule(this);
        }
        private void OnDisable()
        {
            axcelCore = GetComponent<AxcelCore>();
            axcelCore.RemoveModule(this);
        }

        void Update()
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
                Value = Vector2.up * Mathf.Sqrt(2 * Physics2D.gravity.magnitude * 2f);
            }
        }

        private void FixedUpdate()
        {
            
        }
    }
}
