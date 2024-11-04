using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.velocity = new Vector2(0, 0);
        joint.enabled = false;
        rope.enabled = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (horizontal != 0 && !player.HorizontalWallDetected() || horizontal == player.facingDirection * -1)
        {
            stateMachine.ChangeState(player.moveState);
        }
    }
}
