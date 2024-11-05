using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
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

        if (Input.GetKey(KeyCode.Mouse1) && player.skill.control.CanUseSkill() && player.skill.control.controlUnlocked == true)
        {
            if (player.boxController != null)
            {
                player.boxController.GravityOrientation(true);
                player.ControlEffect.SetActive(true);
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse1) && player.BoxGravityControlDetected() && player.skill.control.CanUseSkill() && player.skill.control.controlUnlocked == true)
        {
            stateMachine.ChangeState(player.gravityControlState);
            player.boxController.GravityOrientation(false);
        }
        if (Input.GetKeyDown(KeyCode.E) && player.IsBoxDetected() == true && player.isPushing == false && player.boxController.GroundDetected() && player.boxController.canPush == true) 
        {
            stateMachine.ChangeState(player.pushState);
        }
        if (!player.isGrounded)
        {
            stateMachine.ChangeState(player.airState);
        }
        if (Input.GetButtonDown("Jump") && player.isGrounded && !player.isFloors)
        {
            stateMachine.ChangeState(player.jumpState);
        }
    }
}
