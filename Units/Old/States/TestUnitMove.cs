using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUnitMove : UnitState
{
    //public Rigidbody2D RB { get; private set; }
   
    //private float attackFinishTime;

    private Vector3 startPosition;
    public TestUnitMove(TestUnit testUnit, UnitStateMachine unitStateMachine, TestUnitData unitData, string animBoolName)
        : base(testUnit, unitStateMachine, unitData, animBoolName)
    {
        //startPosition = RTSController.;
        //attackFinishTime = testUnit.UnitAttackState.attackFinishTime;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        //Debug.Log("Enemy in range " + isEnemy);
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
       

        //float facingDirection = (startPosition.x - testUnit.transform.position.x);
        Vector2 velocityVector = (testUnit.RB.velocity);
        Vector2 velocityNormal = (velocityVector).normalized;
        int velocityXNormal = Mathf.RoundToInt(velocityNormal.x);
        core.Movement.CheckIfShouldFlip(velocityXNormal);
        
        if (testUnit.RB.velocity == Vector2.zero)// && !isEnemy)
        {
            unitStateMachine.ChangeState(testUnit.UnitIdleState);
        }
        //if(isEnemy && Time.unscaledTime > testUnit.UnitAttackState.attackFinishTime + unitData.attackDelay)
        
    }
    public void MoveTo(Vector3 targetPosition)
    {
        //targetPosition.DirectPath.SetMovePosition(targetPosition);
        core.DirectPath.SetMovePosition(targetPosition, unitData.moveSpeed);
       
        //core.Movement.SetPosition(targetPosition);
        //Vector3 velocityVector = (targetPosition - testUnit.transform.position).normalized;
        //if (Vector3.Distance(targetPosition, testUnit.transform.position) < 1f)
        //{
        //    velocityVector = Vector3.zero;
        //    //core.Movement.Velocity(velocityVector, unitData.moveSpeed);
        //}
        ////RB.velocity = velocityVector * unitData.moveSpeed;
        //core.Movement.Velocity(velocityVector, unitData.moveSpeed);
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
