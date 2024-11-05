using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxIdleState : BoxState
{
    public BoxIdleState(BoxStateMachine _stateMachine, BoxController _pushable) : base(_stateMachine, _pushable)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 0.1f;
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (!boxController.VasicouisDetected() && !boxController.GroundDetected())
        {
            stateMachine.ChangeState(boxController.boxAirState);
        }
    }
}
