using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : UnitManager
{
    [SerializeField] UnitTypeSO unitType;
    public Animator Anim { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Core Core { get; private set; }
    protected Movement moveToPosition;
    private float lookForTargetTimer;
    //private float lookForTargetTimeMax = .5f;
    private float attackTimerDelay;
    private EnemyController targetEnemy;
    //private string animBoolName;

    [SerializeField] TestUnitData UnitData;
    [SerializeField] Transform attackPosition;


    private void Awake()
    {
        moveToPosition = GetComponent<Movement>();
    }
    protected override void Start()
    {
        Core = GetComponentInChildren<Core>();
        Anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        //animBoolName = "idle";
    }

    void Update()
    {
        Core.LogicUpdate();
        if(RB.velocity == Vector2.zero)
        {
            //idle
        }
        else if (RB.velocity != Vector2.zero)
        {
            //move
        }
    }   
    public void MoveTo(Vector3 targetPosition)
    {
        //moveToPosition.SetPosition(targetPosition);
        //moveToPosition = new Vector3 Core.Movement.SetPosition(targetPosition);
        Core.DirectPath.SetMovePosition(targetPosition, UnitData.moveSpeed);
    }
    public void MoveToAttack(Vector3 targetPosition)
    {
        Vector2 velocityVector = (targetPosition - transform.position).normalized;
        RB.velocity = velocityVector * UnitData.moveSpeed;
    }
    private void CheckIfInAttackRange()
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, unitType.attackRadius);
        foreach (Collider2D collider2D in collider2DArray)
        {
            EnemyController enemy = collider2D.GetComponent<EnemyController>();
            if (enemy != null)
            {
                RB.velocity = Vector2.zero;
                AttackTargets();
            }
        }
    }
    private void AttackTargets()
    {
        //Handles attack
        attackTimerDelay -= Time.deltaTime;
        if (attackTimerDelay <= 0f)
        {
            attackTimerDelay += unitType.attackTimerDelayMax;
            //Instantiate "bullet"
            if (targetEnemy != null)
            {
                //EnemyProjectileHandler.Create(transform.position, targetEnemy);
            }
            //attackTimerDelay = enemyType.attackTimerDelayMax;
        }
    }
}
