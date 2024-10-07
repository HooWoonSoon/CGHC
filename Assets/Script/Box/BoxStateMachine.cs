using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxStateMachine 
{
    public BoxState currentState { get; private set; }
    public void Initialize(BoxState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(BoxState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}
