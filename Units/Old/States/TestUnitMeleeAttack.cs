using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUnitMeleeAttack : TestUnitAttackState
{
    protected AttackDetails attackDetails;
    private bool isEnemy;
    public TestUnitMeleeAttack(TestUnit testUnit, UnitStateMachine unitStateMachine, TestUnitData unitData, string animBoolName, Transform attackPosition) : base(testUnit, unitStateMachine, unitData, animBoolName, attackPosition)
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

        attackDetails.damageAmount = unitData.attackDamage;
        //attackDetails.position = testUnit.transform.attackPosition.position;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isAnimationFinished)
        {
            //attackFinishTime = Time.unscaledTime;

            unitStateMachine.ChangeState(testUnit.UnitIdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, unitData.attackRadius, unitData.whatIsEnemy);
        foreach (Collider2D collider in detectedObjects)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();
            if(damageable != null)
            {
                damageable.Damage(attackDetails.damageAmount);
            }
            //Debug.Log("Enemy detected");
        }
    }
}
