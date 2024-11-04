using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
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
        if (Input.GetKeyDown(KeyCode.Space) && player.isGrounded == false)
        {
            stateMachine.ChangeState(player.wallJumpState);
        }

        if (horizontal != 0 && player.facingDirection != horizontal)
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (player.isGrounded)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
