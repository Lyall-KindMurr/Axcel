using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//logic required so some fields are readonly
public class ReadOnlyAttribute : PropertyAttribute{}
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))] public class ReadOnlyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property,
                                            GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position,
                               SerializedProperty property,
                               GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}


namespace AccelEngine
{
    public class AccelCore : MonoBehaviour
    {
        [ReadOnly] public Vector3[] CheckPositions = new Vector3[8];
        [ReadOnly] public Vector3 LastVelocity = Vector3.zero;
        
        private CapsuleCollider2D col;
        private Rigidbody2D rb;

        // set to public to allow direccct setting from other scripts, such as unity's new input system
        [Header("Current inputs", order = 0)]
        [ReadOnly] public bool ActionKey1;
        [ReadOnly] public bool ActionKey2;
        [ReadOnly] public bool ActionKey3;
        [ReadOnly] [Range(-1.0f, 1.0f)] public float Xinput1;
        [ReadOnly] [Range(-1.0f, 1.0f)] public float Yinput1;
        [ReadOnly] [Range(-1.0f, 1.0f)] public float Xinput2;
        [ReadOnly] [Range(-1.0f, 1.0f)] public float Yinput2;

        [Header("Connected components", order = 1)]
        [ReadOnly] public AccelAnimationCore accAnim;
        [ReadOnly] public IInputManager inputManager;
        [ReadOnly] private readonly List<IMovementModifier> modules = new List<IMovementModifier>(); //unity does not allow serialization of lists

        private void FixedUpdate() => Move();

        public void AddModule(IMovementModifier modifier) => modules.Add(modifier);
        public void RemoveModule(IMovementModifier modifier) => modules.Remove(modifier);

        private void Awake()
        {
            // make sure there is an input manager, and that its active
            inputManager = this.GetComponent<IInputManager>();
            Behaviour _inputManager = (Behaviour)this.GetComponent<IInputManager>();
            if (_inputManager.enabled != true)
            {
                _inputManager.enabled = true;
            }
            if (inputManager == null)
            {
                Debug.Log("Error initiating Engine, no input manager found, adding default one");
                gameObject.AddComponent<ExampleInputFeeder>();
            }

            // get a rb2d
            rb = this.GetComponent<Rigidbody2D>();
            if(rb == null)
            {
                rb = gameObject.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
            }   
            
            // math for checkpoint generation is too complex if it can allow both capsule and box 2d
            if(this.GetComponent<CapsuleCollider2D>() == null)
            {
                Debug.Log("Error initiating Engine, no CapsuleCollider2D found");
                Destroy(this);
            }
            col = this.GetComponent<CapsuleCollider2D>();
            //we place the transforms for boundary checks
            // these should go in a clockwise pattern, with 0 being the top
            {                
                CheckPositions[0] = new Vector3(0f, col.size.y / 2 + col.offset.y, 0f);
                CheckPositions[1] = new Vector3(col.size.x / 2 + col.offset.x, col.size.y / 2 + col.offset.y - 0.05f, 0f);
                CheckPositions[2] = new Vector3(col.size.x / 2 + col.offset.x, 0f, 0f);
                CheckPositions[3] = new Vector3(col.size.x / 2 + col.offset.x, -col.size.y / 2 + col.offset.y + 0.05f, 0f);
                CheckPositions[4] = new Vector3(0f, -col.size.y / 2 + col.offset.y, 0f);
                CheckPositions[5] = new Vector3(-col.size.x / 2 + col.offset.x, -col.size.y / 2 + col.offset.y + 0.05f, 0f);
                CheckPositions[6] = new Vector3(-col.size.x / 2 + col.offset.x, 0f, 0f);
                CheckPositions[7] = new Vector3(-col.size.x / 2 + col.offset.x, col.size.y / 2 + col.offset.y - 0.05f, 0f);
            }
            accAnim = GetComponentInChildren(typeof(AccelAnimationCore), false) as AccelAnimationCore;
        }


        private void Move()
        {
            Vector3 _velocity = rb.velocity;

            foreach(IMovementModifier module in modules)
            {
                if (module.Absolute)
                {
                    rb.isKinematic = true;
                    _velocity = module.Value;
                    break; // it is useless to compute other modules if we found an absolute one
                }
                else
                {
                    rb.isKinematic = false;
                    _velocity += module.Value;
                }
            }

            if (_velocity != Vector3.zero)
            {
                rb.velocity = _velocity;
            }
        }

        private void Update()
        {
            // we take the input in update for precise input management
            ActionKey1 = inputManager.ActionKey1;
            ActionKey2 = inputManager.ActionKey2;
            ActionKey3 = inputManager.ActionKey3;
            Xinput1 = inputManager.Xinput1;
            Yinput1 = inputManager.Yinput1;
            Xinput2 = inputManager.Xinput2;
            Yinput2 = inputManager.Yinput2;
        }
    }
}
