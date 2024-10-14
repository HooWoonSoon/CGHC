using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.CheckForJumpCount(true);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
   
        if (Input.GetKeyDown(KeyCode.E) && player.ControlTrigger() == true && player.isPushing == false && player.pushable.IsGrounded())
        {
            stateMachine.ChangeState(player.pushState);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && player.ControlTrigger() == true)
        {
            stateMachine.ChangeState(player.gravityControlState);
        }
        if (!player.isGrounded)
        {
            stateMachine.ChangeState(player.airState);
        }
        if (jump > 0 && player.isGrounded)
        {
            stateMachine.ChangeState(player.jumpState);
        }
    }
}
