using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStateMachine
{
    public UnitState CurrentState { get; private set; }

    public void Initialize(UnitState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }
    public void ChangeState(UnitState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
