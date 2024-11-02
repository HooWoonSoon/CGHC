using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public virtual bool CanUseSkill()
    {
        return true;
    }

    public virtual void UseSkill()
    {

    }
}
