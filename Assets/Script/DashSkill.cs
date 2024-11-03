using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSkill : Skill
{
    [Header("Dash")]
    public bool dashUnlocked;

    public void UnlockDash()
    {
        dashUnlocked = true;
    }
}
