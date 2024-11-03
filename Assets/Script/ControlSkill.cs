using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSkill : Skill
{
    [Header("Control")]
    public bool controlUnlocked;

    public void UnlockDash()
    {
        controlUnlocked = true;
    }
}
