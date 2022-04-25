using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ReadOnlyAttribute : PropertyAttribute{}

namespace AccelEngine
{
    public class AxcelCore : MonoBehaviour
    {
        [SerializeField] ////// PLEASE DESERIALIZE THIS WHEN POSSIBLE
        public readonly Vector3[] CheckPositions = new Vector3[8];
        public Vector3 LastVelocity = Vector3.zero;
        
        private CapsuleCollider2D col;
        private Rigidbody2D rb;

        [Header("Current inputs")]
        [ReadOnly] public bool ActionKey1;
        [ReadOnly] public bool ActionKey2;
        [ReadOnly] public bool ActionKey3;
        [ReadOnly] [Range(0.0f, 1.0f)] public float Xinput1;
        [ReadOnly] [Range(0.0f, 1.0f)] public float Yinput1;
        [ReadOnly] [Range(0.0f, 1.0f)] public float Xinput2;
        [ReadOnly] [Range(0.0f, 1.0f)] public float Yinput2;

        [Header("Connected components")][SerializeReference]
        private readonly List<IMovementModifier> modules = new List<IMovementModifier>();


        private void FixedUpdate() => Move();

        public void AddModule(IMovementModifier modifier) => modules.Add(modifier);
        public void RemoveModule(IMovementModifier modifier) => modules.Remove(modifier);

        private void Awake()
        {
            rb = this.GetComponent<Rigidbody2D>();
            if(rb == null)
            {
                rb = gameObject.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
            }
            col = this.GetComponent<CapsuleCollider2D>();
            if(col == null)
            {
                Debug.Log("Error initiating Engine, no CapsuleCollider2D found");
                Destroy(this);
            }
            //we place the transforms for boundary checks
            // these should go in a clockwise pattern, with 0 being the top
            {                
                CheckPositions[0] = new Vector3(0f, col.size.y / 2 + col.offset.y, 0f);
                CheckPositions[1] = new Vector3(col.size.x / 2 + col.offset.x, col.size.y / 2 + col.offset.y, 0f);
                CheckPositions[2] = new Vector3(col.size.x / 2 + col.offset.x, 0f, 0f);
                CheckPositions[3] = new Vector3(col.size.x / 2 + col.offset.x, -col.size.y / 2 + col.offset.y, 0f);
                CheckPositions[4] = new Vector3(0f, -col.size.y / 2 + col.offset.y, 0f);
                CheckPositions[5] = new Vector3(-col.size.x / 2 + col.offset.x, -col.size.y / 2 + col.offset.y, 0f);
                CheckPositions[6] = new Vector3(-col.size.x / 2 + col.offset.x, 0f, 0f);
                CheckPositions[7] = new Vector3(-col.size.x / 2 + col.offset.x, col.size.y / 2 + col.offset.y, 0f);
            }
        }


        private void Move()
        {
            Vector3 _velocity = Vector3.zero;

            foreach(IMovementModifier module in modules)
            {
                _velocity += module.Value;
            }

            rb.velocity = _velocity;
            LastVelocity = _velocity;
        }

        // Update is maintained for future animation possibility
        void Update()
        {

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("hit something " + collision.collider.name); //othercollider returns this items name oddly
        }
    }
}
