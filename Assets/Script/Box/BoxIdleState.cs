using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxIdleState : BoxState
{
    public BoxIdleState(BoxStateMachine _stateMachine, Pushable _pushable) : base(_stateMachine, _pushable)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
}
