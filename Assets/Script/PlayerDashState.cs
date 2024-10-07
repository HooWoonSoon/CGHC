using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.dashDuration;
        player.isDash = true;
    }

    public override void Exit()
    {
        base.Exit();

        player.SetVelocity(0, rb.velocity.y);
        player.isDash = false;
    }

    public override void Update()
    {
        base.Update();
        if (!player.isGrounded && player.isWalled)
        {
            stateMachine.ChangeState(player.wallSlideState);
        }

        player.SetVelocity(player.dashSpeed * player.dashDirection, 0);

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
