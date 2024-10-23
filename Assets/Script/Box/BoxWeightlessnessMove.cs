using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BoxWeightlessnessMove : BoxState
{
    public BoxWeightlessnessMove(BoxStateMachine _stateMachine, BoxController _pushable) : base(_stateMachine, _pushable)
    {
    }

    public override void Enter()
    {
        base.Enter();
        boxController.orentaition = boxController.wayMove;
        boxController.rb.gravityScale = 0;
        boxController.ChangeGroundTranform();
    }

    public override void Exit()
    {
        base.Exit();
        boxController.rb.gravityScale = 2;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixeUpdate()
    {
        base.FixeUpdate();
        rb.MovePosition(boxController.transform.position + boxController.orentaition * 2f * Time.deltaTime);
        if (boxController.VasicouisDetected())
        {
            stateMachine.ChangeState(boxController.boxIdleState);
        }
    }
}
