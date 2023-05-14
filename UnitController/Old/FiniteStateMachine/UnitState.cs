using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState
{
    protected Core core;

    protected TestUnit testUnit;
    protected UnitStateMachine unitStateMachine;
    protected TestUnitData unitData;
    protected float startTime;

    private string animBoolName;

    public UnitState(TestUnit testUnit, UnitStateMachine unitStateMachine, TestUnitData unitData, string animBoolName)
    {
        this.testUnit = testUnit;
        this.unitStateMachine = unitStateMachine;
        this.unitData = unitData;
        this.animBoolName = animBoolName;
        core = testUnit.Core;
    }

    public virtual void Enter()
    {
        DoChecks();

        testUnit.Anim.SetBool(animBoolName, true);
        startTime = Time.time;
    }
    public virtual void DoChecks()
    {

    }

    public virtual void LogicUpdate()
    { }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    public virtual void Exit()
    {
        testUnit.Anim.SetBool(animBoolName, false);
    }
}
