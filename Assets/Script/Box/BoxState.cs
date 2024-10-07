using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxState
{
    protected BoxStateMachine stateMachine;
    protected Pushable pushable;
    protected Rigidbody2D rb;

    public BoxState(BoxStateMachine _stateMachine, Pushable _pushable)
    {
        this.pushable = _pushable;
        this.stateMachine = _stateMachine;
    }

    public virtual void Enter()
    {
        Debug.Log("I am in" + stateMachine.currentState);
        rb = pushable.rb;
    }

    public virtual void Exit()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void FixeUpdate()
    {

    }
}
