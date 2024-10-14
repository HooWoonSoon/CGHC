using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (player.isWalled)
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
        if (player.isGrounded)
        {
            stateMachine.ChangeState(player.idleState);
        }
        if (horizontal != 0)
        {
            player.SetVelocity(player.moveSpeed * horizontal, rb.velocity.y);
        }
        if (player.leftJump > 0 && jump > 0)
        {
            stateMachine.ChangeState(player.jumpState);
        }
    }
}
