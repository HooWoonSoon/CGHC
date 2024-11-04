using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    private float jumpHoldTime;
    public PlayerJumpState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        jumpHoldTime = 0;
        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void FixeUpate()
    {
        base.FixeUpate();
        Debug.Log(jumpHoldTime);
        if (Input.GetButton("Jump"))
        {
            jumpHoldTime += Time.deltaTime;

            if (jumpHoldTime < player.maxJumpHoldTime)
            {
                rb.AddForce(Vector2.up * player.jumpForce * 0.6f, ForceMode2D.Impulse);
            }
        }

        if (Input.GetButtonUp("Jump") || jumpHoldTime >= player.maxJumpHoldTime)
        {
            stateMachine.ChangeState(player.airState);
        }
    }
    public override void Update()
    {
        if (rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.airState);
        }
    }
}
