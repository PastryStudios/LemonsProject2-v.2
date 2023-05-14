using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUnitIdle : UnitState
{
    protected Vector3 moveToPosition;
    private bool isEnemy;
    private bool canAttack;


    public TestUnitIdle(TestUnit testUnit, UnitStateMachine unitStateMachine, TestUnitData unitData, string animBoolName) 
        : base(testUnit, unitStateMachine, unitData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
        isEnemy = core.CollisionSenses.Enemy;
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        canAttack = testUnit.UnitAttackState.CanAttack;

        //moveToPosition = testUnit.MoveTo();
        if (testUnit.RB.velocity != Vector2.zero)
        {
            unitStateMachine.ChangeState(testUnit.UnitMoveState);
        }
        //if (isEnemy && Time.unscaledTime > testUnit.UnitAttackState.attackFinishTime + unitData.attackDelay)
        if (isEnemy && canAttack)
        {
            //testUnit.RB.velocity = Vector2.zero;
            unitStateMachine.ChangeState(testUnit.UnitMeleeAttackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
