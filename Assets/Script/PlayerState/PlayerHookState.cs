using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerHookState : PlayerState
{
    public PlayerHookState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
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
        rope.SetPosition(1, player.transform.position);
        if (Input.GetMouseButtonUp(0))
        {
            joint.enabled = false;
            rope.enabled = false;
        }
        if (Input.GetKey(KeyCode.W))
        {
            joint.distance = Mathf.Max(0.5f, joint.distance - player.climbSpeed * Time.deltaTime);
        }
        if (horizontal != 0)
        {
            player.FlipController(horizontal);
            player.ApplySwingForce();
        }
        if (player.isGrounded)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
