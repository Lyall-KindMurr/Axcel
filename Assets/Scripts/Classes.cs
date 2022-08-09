using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState
{
    virtual public CharacterState HandleState(string key)
    {
        return this;
    }
}

public class JumpingState : CharacterState
{
    override public CharacterState HandleState(string key)
    {
        if (key == "Attack")
            return new AttackState();

        return this;
    }
}

public class AttackState : CharacterState
{
    override public CharacterState HandleState(string key)
    {
        if (key == "NotAttack")
            return new IdleState();

        return this;
    }
}

public class IdleState : CharacterState
{
    override public CharacterState HandleState(string key)
    {
        if (key == "Attack")
            return new AttackState();

        return this;
    }
}
