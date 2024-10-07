using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravityControlState : PlayerState
{
    public PlayerGravityControlState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.pushable.boxStateMachine.ChangeState(player.pushable.boxWeightlessnessMove);

        player.SetVelocity(rb.velocity.x, 10);

        stateTimer = 0.1f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            rb.velocity = new Vector2(0,0);

        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
