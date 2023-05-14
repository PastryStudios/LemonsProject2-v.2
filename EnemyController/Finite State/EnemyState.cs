using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState 
{
    protected Core core;

    protected EnemyUnit enemyUnit;
    protected EnemyStateMachine enemyStateMachine;
    protected EnemyData enemyData;

    private string animBoolName;

    public EnemyState(EnemyUnit enemyUnit, EnemyStateMachine enemyStateMachine, EnemyData enemyData, string animBoolName)
    {
        this.enemyUnit = enemyUnit;
        this.enemyStateMachine = enemyStateMachine;
        this.enemyData = enemyData;
        this.animBoolName = animBoolName;
        core = enemyUnit.Core;
    }
    public virtual void Enter()
    {
        DoChecks();

        enemyUnit.Anim.SetBool(animBoolName, true);
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
        enemyUnit.Anim.SetBool(animBoolName, false);
    }
}
