using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookSkill : Skill
{
    [Header("Hook")]
    public bool hookUnlocked;

    public void UnlockControl()
    {
        hookUnlocked = true;
    }
}
