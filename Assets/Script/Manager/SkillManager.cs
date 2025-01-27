using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public ControlSkill control;
    public DoubleJumpSkill doubleJump;
    public DashSkill dash;
    public HookSkill hook;
    private void Awake()
    {
        if (instance != null) 
            Destroy(instance);
        else
            instance = this;
    }

    private void Start()
    {
        dash = GetComponent<DashSkill>();
        control = GetComponent<ControlSkill>();
        hook = GetComponent<HookSkill>();
        doubleJump = GetComponent<DoubleJumpSkill>();
    }
}
