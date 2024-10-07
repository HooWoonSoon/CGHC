using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJumpState : PlayerState
{
    public PlayerDoubleJumpState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(rb.velocity.x, player.jumpForce * 0.8f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.airState);
        }
    }
}
