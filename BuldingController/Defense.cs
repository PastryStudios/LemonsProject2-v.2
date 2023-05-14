using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : MonoBehaviour
{
    private BuildingTypeSO buildingType;

    private Enemy targetEnemy;

    private float lookForTargetTime;
    private float lookForTargetTimeMax = 1f;
    private float attackTimerDelay;
    private float bulletDelay;


    private void Awake()
    {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
    }
    private void Update()
    {
        HandleTargeting();
        HandleShooting();
    }
    private void HandleTargeting()
    {
        lookForTargetTime -= Time.deltaTime;
        if(lookForTargetTime < 0f)
        {
            lookForTargetTime += lookForTargetTimeMax;
            LookForTargets();
        }
    }
    private void HandleShooting()
    {
        bulletDelay -= Time.deltaTime;
        if(bulletDelay < 0)
        {
            bulletDelay += buildingType.bulletDelayMax;
            if(targetEnemy != null)
            {
                BulletHandler.Create(transform.position, targetEnemy);
            }
        }
    }

    private void LookForTargets()
    {
        float targetMaxRadius = 20f;
        Collider2D[] collider2Darray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);
        foreach (Collider2D collider2D in collider2Darray)
        {
            Enemy enemy = collider2D.GetComponent<Enemy>();
            if(enemy != null)
            {
                if(targetEnemy == null)
                {
                    targetEnemy = enemy;
                }
                else
                {
                    if(Vector3.Distance(transform.position, enemy.transform.position) <
                        Vector3.Distance(transform.position, targetEnemy.transform.position))
                    {
                        targetEnemy = enemy;
                    }
                }
            }
        }
    }
    private void AttackTargets()
    {
        //Handles attack
        attackTimerDelay -= Time.deltaTime;
        if (attackTimerDelay <= buildingType.attackTimerDelayMax)
        {
            attackTimerDelay += buildingType.attackTimerDelayMax;
            //Instantiate "bullet"
            if (targetEnemy != null)
            {
                BulletHandler.Create(transform.position, targetEnemy);
            }
            //attackTimerDelay = enemyType.attackTimerDelayMax;
        }
    }
}
