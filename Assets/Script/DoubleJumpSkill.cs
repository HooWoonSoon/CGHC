using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpSkill : Skill
{
    [Header("Double Jump")]
    public bool doubleJumpUnlock;

    public void UnlockDoubleJump()
    {
        doubleJumpUnlock = true;
    }
}
