using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BoxWeightlessnessMove : BoxState
{
    public BoxWeightlessnessMove(BoxStateMachine _stateMachine, Pushable _pushable) : base(_stateMachine, _pushable)
    {
    }

    public override void Enter()
    {
        base.Enter();
        pushable.orentaition = pushable.pointCheck.wayMove;
        pushable.rb.bodyType = RigidbodyType2D.Dynamic;
        pushable.rb.gravityScale = 0;
        pushable.ChangeGroundTranform();
    }

    public override void Exit()
    {
        base.Exit();
        pushable.rb.gravityScale = 2;
        pushable.rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixeUpdate()
    {
        base.FixeUpdate();
        rb.MovePosition(pushable.transform.position + pushable.orentaition * 2.5f * Time.deltaTime);
    }
}
