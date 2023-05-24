using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUnitMoveToAttack : TestUnitAttackState
{
    private bool isEnemy;
    private bool canAttack;
    public TestUnitMoveToAttack(TestUnit testUnit, UnitStateMachine unitStateMachine, TestUnitData unitData, string animBoolName, Transform attackPosition) : base(testUnit, unitStateMachine, unitData, animBoolName, attackPosition)
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
        Vector2 velocityVector = (testUnit.RB.velocity);
        Vector2 velocityNormal = (velocityVector).normalized;
        int velocityXNormal = Mathf.RoundToInt(velocityNormal.x);
        core.Movement.CheckIfShouldFlip(velocityXNormal);

        if (isEnemy && canAttack)// && !isEnemy)
        {
            unitStateMachine.ChangeState(testUnit.UnitMeleeAttackState);
        }
    }
    public void MoveTo(Vector3 targetPosition)
    {
        //targetPosition.DirectPath.SetMovePosition(targetPosition);
        core.DirectPath.SetMovePosition(targetPosition, unitData.moveSpeed);
        //if (isEnemy)
        //{
        //    testUnit.RB.velocity = Vector2.zero;
        //}
        if (isEnemy && canAttack)
        {
            Debug.Log("Is Enemy " + isEnemy);
            testUnit.RB.velocity = Vector2.zero;
            unitStateMachine.ChangeState(testUnit.UnitMeleeAttackState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
