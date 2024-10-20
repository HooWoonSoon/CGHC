using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxAirState : BoxState
{
    public BoxAirState(BoxStateMachine _stateMachine, BoxController _pushable) : base(_stateMachine, _pushable)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.isKinematic = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (boxController.GroundDetected())
        {
            stateMachine.ChangeState(boxController.boxIdleState);
        }
    }
}
