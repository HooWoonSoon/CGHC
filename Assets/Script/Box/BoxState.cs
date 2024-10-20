using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxState
{
    protected BoxStateMachine stateMachine;
    protected BoxController boxController;
    protected Rigidbody2D rb;

    public BoxState(BoxStateMachine _stateMachine, BoxController _pushable)
    {
        this.boxController = _pushable;
        this.stateMachine = _stateMachine;
    }

    public virtual void Enter()
    {
        rb = boxController.rb;
        Debug.Log("I am in" + stateMachine.currentState);
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
