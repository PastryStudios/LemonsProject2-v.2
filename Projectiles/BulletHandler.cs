using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    public static BulletHandler Create(Vector3 position, Enemy targetEnemy)
    {
        Transform pfBullet = Resources.Load<Transform>("pfBullet");
        //buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);

        Transform bulletTransform = Instantiate(pfBullet, position, Quaternion.identity);
        BulletHandler bulletProjectile = bulletTransform.GetComponent<BulletHandler>();
        bulletProjectile.SetTarget(targetEnemy);
        return bulletProjectile;
    }

    private Vector3 lastMoveDir;
    private float timeToDie = 3f;

    private Enemy targetEnemy;

    private void Update()
    {
        Vector3 moveDir;
        if (targetEnemy != null)
        {
            moveDir = (targetEnemy.transform.position - transform.position).normalized;
            lastMoveDir = moveDir;
        }
        else
        {
            moveDir = lastMoveDir;
        }
        float moveSpeed = 10f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        timeToDie -= Time.deltaTime;
        if (timeToDie <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void SetTarget(Enemy targetEnemy)
    {
        this.targetEnemy = targetEnemy;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        EnemyGenerator enemyGenerator = collision.GetComponent<EnemyGenerator>();
        int damageAmount = 10;
        if (enemy != null)
        {
            //Enemy Hit
            enemy.GetComponent<HealthSystem>().Damage(damageAmount);
            Destroy(gameObject);
        }
        if (enemyGenerator != null)
        {
            //Enemy Hit
            enemyGenerator.GetComponent<HealthSystem>().Damage(damageAmount);
            Destroy(gameObject);
        }
    }
}
