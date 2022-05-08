using UnityEngine;

namespace AccelEngine
{
    public interface IMovementModifier
    {
        Vector3 Value { get; } // the value of rigidbody velocity returned by the module

        bool Absolute { get; } // the type of movement, true for kinematic, false for additive
        bool Priority { get; } // is this a solo script
    }

    public interface IInputManager
    {
        //output the ABC buttons and X/Y
        bool ActionKey1 { get; set; }
        bool ActionKey2 { get; set; }
        bool ActionKey3 { get; set; }
        float Xinput1 { get; set; }
        float Yinput1 { get; set; }
        float Xinput2 { get; set; }
        float Yinput2 { get; set; }
    }
}
