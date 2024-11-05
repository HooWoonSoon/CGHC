using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 0.4f;
        player.SetVelocity(5 * -player.facingDirection, player.jumpForce * 1.5f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.airState);
        }

        if (player.isGrounded)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
