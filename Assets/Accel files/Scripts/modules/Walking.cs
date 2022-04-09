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

        private void OnEnable()
        {
            axcelCore = GetComponent<AxcelCore>();
            axcelCore.AddModule(this);
        }

        // Update is called once per frame
        void Update()
        {
            tester = axcelCore.CheckPositions[0];
        }
    }
}
