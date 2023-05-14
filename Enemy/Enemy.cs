using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyTypeSO enemyType;

    private HealthSystem healthSystem;
    private BuildingController targetBuilding;
    private Rigidbody2D RB;
    private float lookForTargetTimer;
    private float lookForTargetTimeMax = .5f;
    private float attackTimerDelay;

    private void Start()
    {
        targetBuilding = BuildingManager.Instance.GetHQBuilding();
        RB = GetComponent<Rigidbody2D>();
        healthSystem = GetComponent<HealthSystem>();


        lookForTargetTimer = Random.Range(0f, lookForTargetTimeMax);
        //healthSystem.OnDeath += HealthSystem_OnDeath;
        //healthSystem.OnDamaged += HealthSystem_OnDamaged;
    }
    //private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    //{
    //    Debug.Log(healthSystem.GetHealthAmount());
    //}
    //private void HealthSystem_OnDeath(object sender, System.EventArgs e)
    //{
    //    Destroy(gameObject);
    //}
    private void Update()
    {
        lookForTargetTimer -= Time.deltaTime;
        if(targetBuilding != null)
        {
            Vector3 moveDir = (targetBuilding.transform.position - transform.position).normalized;
            RB.velocity = moveDir * enemyType.moveSpeed;
        }
        else
        {
            RB.velocity = Vector2.zero;
            LookForTargets();
        }
        lookForTargetTimer -= Time.deltaTime;
        if(lookForTargetTimer < 0f)
        {
            lookForTargetTimer += lookForTargetTimeMax;
            LookForTargets();
        }
        CheckIfInAttackRange();
    }
    private void LookForTargets()
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, enemyType.detectionRadius);
        foreach(Collider2D collider2D in collider2DArray)
        {
            BuildingController building = collider2D.GetComponent<BuildingController>();
            if(building != null)
            {
                if(targetBuilding == null)
                {
                    targetBuilding = building;
                }
                else
                {
                    if(Vector3.Distance(transform.position, building.transform.position) < 
                        Vector3.Distance(transform.position, targetBuilding.transform.position))
                    {
                        targetBuilding = building;
                    }
                }
            }
        }
        if(targetBuilding == null)
        {
            targetBuilding = BuildingManager.Instance.GetHQBuilding();
        }
    }
    private void CheckIfInAttackRange()
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, enemyType.attackRadius);
        foreach (Collider2D collider2D in collider2DArray)
        {
            BuildingController building = collider2D.GetComponent<BuildingController>();
            if(building != null)
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
        if(attackTimerDelay <= 0f)
        {
            attackTimerDelay += enemyType.attackTimerDelayMax;
            //Instantiate "bullet"
            if(targetBuilding != null)
            {
                EnemyProjectileHandler.Create(transform.position, targetBuilding);
            }
            //attackTimerDelay = enemyType.attackTimerDelayMax;
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, enemyType.detectionRadius);
        Gizmos.color = Color.blue; 
        Gizmos.DrawWireSphere(transform.position, enemyType.attackRadius);
        Gizmos.color = Color.yellow;

    }
}
