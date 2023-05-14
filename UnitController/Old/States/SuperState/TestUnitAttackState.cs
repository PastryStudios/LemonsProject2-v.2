using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUnitAttackState : UnitState
{
    protected Transform attackPosition;
    protected bool isAnimationFinished;
    protected float attackDelay;
    public float attackFinishTime;
    public bool CanAttack;
    //public bool CanAttack;
    public TestUnitAttackState(TestUnit testUnit, UnitStateMachine unitStateMachine, TestUnitData unitData, string animBoolName, Transform attackPosition) 
        : base(testUnit, unitStateMachine, unitData, animBoolName)
    {
        this.attackPosition = attackPosition;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        testUnit.Atsm.attackState = this;
        isAnimationFinished = false;
        CanAttack = true;
        testUnit.RB.velocity = Vector2.zero;
        attackFinishTime = Time.unscaledTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //if (Time.time > attackFinishTime + unitData.attackDelay)
        //{
        //    CanAttack = true;
        //}
        //else
        //{
        //    CanAttack = false;
        //}
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

      

    public virtual void TriggerAttack()
    {

    }
    public virtual void FinishAttack()
    {
        attackFinishTime = Time.unscaledTime;
        CanAttack = false;
        testUnit.CountDown();
        isAnimationFinished = true;
        //CanAttack = false;
        //StartCoroutine(DelayAttack());
    }

    
}
