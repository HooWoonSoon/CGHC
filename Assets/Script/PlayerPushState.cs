using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushState : PlayerState
{
    public PlayerPushState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.isPushing = true;
        SetupPushPosition();
    }
    private void SetupPushPosition()
    {
        float distance = player.skin;
        player.hitBox.transform.SetParent(player.transform);

        player.hitBox.transform.localPosition = player.hitBox.transform.localPosition + new Vector3(distance, 0, 0); ;
    }


    public override void Exit()
    {
        base.Exit();
        player.isPushing = false;
        player.hitBox.transform.SetParent(null);
    }
    public override void Update()
    {
        base.Update();
        if (!player.pushable.IsGrounded())
        {
            stateMachine.ChangeState(player.idleState);
            player.pushable.rb.bodyType = RigidbodyType2D.Dynamic;
            return;
        }
        player.SetVelocity(horizontal * player.moveSpeed * 0.6f, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.E))
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
